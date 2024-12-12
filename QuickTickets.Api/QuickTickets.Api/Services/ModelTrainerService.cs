using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using QuickTickets.Api.Data;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Exceptions;
using System.Data;

namespace QuickTickets.Api.Services
{
    public class ModelTrainerService : BackgroundService
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;
        private readonly IServiceScopeFactory _scopeFactory;
        private static readonly TimeSpan ModelTrainingInterval = TimeSpan.FromMinutes(1);

        public ModelTrainerService(IServiceScopeFactory scopeFactory)
        {
            _mlContext = new MLContext();
            _model = null;
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Wywołanie metody tworzenia modelu predykcji
                    await TrainModel();

                    // Logowanie do konsoli
                    Console.WriteLine($"Model predykcji stworzony o: {DateTime.Now}");
                }
                catch (NoDataException ex)
                {
                    Console.WriteLine($"Trenowanie modelu pominięte: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd podczas tworzenia modelu predykcji: {ex.Message}");
                    throw;
                }

                // Oczekiwanie 5 minut przed kolejnym uruchomieniem
                await Task.Delay(ModelTrainingInterval, stoppingToken);
            }
        }
        // Funkcja do treningu modelu
        public async Task TrainModel()
        {
            Console.WriteLine("Rozpoczynam trening modelu...");
            var mlContext = new MLContext();

            //(IDataView trainingDataView, IDataView testDataView) = LoadData(mlContext);
            (IDataView trainingDataView, IDataView testDataView) = await LoadDataAsync(mlContext);

            if (trainingDataView.GetRowCount() == 0 || testDataView.GetRowCount() == 0) {
                throw new NoDataException("Nie ma danych uczących. Trening nie może zostać wykonany.");
            }

            ITransformer model = BuildAndTrainModel(mlContext, trainingDataView);

            EvaluateModel(mlContext, testDataView, model);

            SaveModel(mlContext, trainingDataView.Schema, model);

            Console.WriteLine("Trening zakończony.");
        }
        public async Task<(IDataView training, IDataView test)> LoadDataAsync(MLContext mlContext)
        {
            try
            {
                List<EventRating> data = new();

                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                    foreach (var eventRating in context.UserEventHistory.ToList()) {
                        data.Add(new EventRating { EventId = eventRating.EventID, Label = eventRating.Label, UserId = eventRating.UserID});
                    }
                }
                if (data != null && data.Any())
                {
                    IDataView dataView = _mlContext.Data.LoadFromEnumerable(data);

                    var splitData = _mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

                    Console.WriteLine("Dane zostały załadowane do MLContext!");
                    return (splitData.TrainSet, splitData.TestSet);
                }
                else
                {
                    Console.WriteLine("Brak danych do załadowania.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania danych z bazy: {ex.Message}");
            }
            IDataView emptyDataView = _mlContext.Data.LoadFromEnumerable(new List<EventRating>());
            return (emptyDataView, emptyDataView);

        }
        public static (IDataView training, IDataView test) LoadData(MLContext mlContext)
        {
            //DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<EventRating>();
            ////string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=<YOUR-DB-FILEPATH>;Database=<YOUR-DB-NAME>;Integrated Security=True;Connect Timeout=30"; //taki byl default
            //string connectionString = "Server=localhost\\SQLEXPRESS;Database=QuickTickets;User ID=tadek;Password=admin;Trusted_Connection=True;Encrypt=False";

            //string sqlCommand = "SELECT CAST(UserID as REAL) as UserID, CAST(EventID as REAL) as EventID, CAST(Label as REAL) as Label FROM UserEventHistory";

            //DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance, connectionString, sqlCommand);

            //IDataView data = loader.Load(dbSource);

            //var splitData = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);

            var trainingData = new List<EventRating>
            {
                new EventRating { UserId = 1, EventId = 1, Label = 1 },
                new EventRating { UserId = 2, EventId = 2, Label = 2 },
                new EventRating { UserId = 3, EventId = 3, Label = 1 },
                new EventRating { UserId = 4, EventId = 4, Label = 1 }
            };

            var testData = new List<EventRating>
            {
                new EventRating { UserId = 3, EventId = 2, Label = 2 }
            };

            IDataView trainingDataView = mlContext.Data.LoadFromEnumerable(trainingData);
            IDataView testDataView = mlContext.Data.LoadFromEnumerable(testData);

            return (trainingDataView, testDataView);
        }
        public static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
        {
            IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: nameof(EventRating.UserId))
                                                 .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "eventIdEncoded", inputColumnName: nameof(EventRating.EventId)));

            // Tworzenie procesu trenowania modelu
            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "userIdEncoded",
                MatrixRowIndexColumnName = "eventIdEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            Console.WriteLine("=============== Training the model ===============");
            ITransformer model = trainerEstimator.Fit(trainingDataView);

            return model;
        }
        public static void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
        {
            Console.WriteLine("=============== Evaluating the model ===============");
            var prediction = model.Transform(testDataView);

            var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");

            Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
            Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
        }
        public static void UseModelForSinglePrediction(MLContext mlContext, ITransformer model)
        {
            Console.WriteLine("=============== Making a prediction ===============");
            var predictionEngine = mlContext.Model.CreatePredictionEngine<EventRating, EventRatingPrediction>(model);

            var testInput = new EventRating { UserId = 2, EventId = 4 };

            var eventRatingPrediction = predictionEngine.Predict(testInput);

            Console.WriteLine("Event " + eventRatingPrediction.Score + " is recommended for user " + eventRatingPrediction.Label);

            if (Math.Round(eventRatingPrediction.Score, 1) > 3.5)
            {
                Console.WriteLine("Event " + testInput.EventId + " is recommended for user " + testInput.UserId);
            }
            else
            {
                Console.WriteLine("Event " + testInput.EventId + " is not recommended for user " + testInput.UserId);
            }
        }
        public static void SaveModel(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
        {
            var modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "EventRecommenderModel.zip");

            Console.WriteLine("=============== Saving the model to a file ===============");
            mlContext.Model.Save(model, trainingDataViewSchema, modelPath);
        }
    }
}
