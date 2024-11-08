using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using Microsoft.Identity.Client;
using Microsoft.ML;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using System.Drawing.Drawing2D;

namespace QuickTickets.Api.Services
{
    public class UserEventHistoryService : IUserEventHistoryService
    {
        private readonly DataContext _context;
        private readonly IEventsService _eventsService;
        private readonly PredictionEnginePool<EventRating, EventRatingPrediction> _predictionEnginePool;
        public UserEventHistoryService(DataContext context, PredictionEnginePool<EventRating, EventRatingPrediction> predictionEnginePool, IEventsService eventsService)
        {
            _context = context;
            _predictionEnginePool = predictionEnginePool;
            _eventsService = eventsService;
        }

        public async Task<IActionResult> Add(AddUserEventHistoryRequestDto addUserEventHistoryRequestDto, Guid userId)
        {
            if (_context.UserEventHistory == null)
            {
                return new NotFoundResult();
            }

            AccountEntity accountEntity = await _context.Accounts.FindAsync(userId);

            var userEventHistoryEntity = new UserEventHistoryEntity
            {
                UserEventHistoryID = 0,
                Label = addUserEventHistoryRequestDto.Label,
                EventID = addUserEventHistoryRequestDto.EventID,
                UserID = int.Parse(accountEntity.ModelID),
            };

            _context.UserEventHistory.Add(userEventHistoryEntity);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<IActionResult> GetPredictedEvents(Guid userId)
        {
            AccountEntity accountEntity = await _context.Accounts.FindAsync(userId);
            long ModelID = int.Parse(accountEntity.ModelID);
            DateTime dateTimeNow = DateTime.Now;
            DateTime dateTimeIn30Days = dateTimeNow.AddDays(30);

            var eventsEntity = await _context.Events.Include(x => x.Owner).Include(x => x.Type).Include(x => x.Location).Where(x => x.IsActive == true && x.Status == StatusEnum.Confirmed.ToString() &&
                x.Date >= dateTimeNow &&
                x.Date <= dateTimeIn30Days).OrderBy(x => x.Date).ToListAsync();
            var predictions = new List<(long EventId, float Score)>();

            foreach (var eventEntity in eventsEntity)
            {
                var testInput = new EventRating { UserId = ModelID, EventId = eventEntity.EventID };

                var eventRatingPrediction = _predictionEnginePool.Predict(modelName: "EventRecommenderModel", testInput);

                predictions.Add((EventId: eventEntity.EventID, Score: eventRatingPrediction.Score));

            }

            var topEvents = predictions.OrderByDescending(p => p.Score).Take(3);
            var ListOfEventsInfo = new List<EventInfoDto>();

            Console.WriteLine($"Top 3 recommended events for user {ModelID}:");
            foreach (var (eventId, score) in topEvents)
            {
                Console.WriteLine($"EventId: {eventId}, Predicted Score: {score}");
                ListOfEventsInfo.Add(_eventsService.GetEventInfoDto(eventsEntity.Find(x=> x.EventID == eventId)));
            }
            
            return new OkObjectResult(ListOfEventsInfo);
        }
    }
}
