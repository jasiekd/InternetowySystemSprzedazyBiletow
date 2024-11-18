using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.ML;
using QuickTickets.Api.Data;
using QuickTickets.Api.Services;
using QuickTickets.Api.Settings;
using System.Text;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string modelPath = Path.GetFullPath(Path.Combine(currentDirectory, "..\\..\\..\\Data\\EventRecommenderModel.zip"));

            builder.Services.AddPredictionEnginePool<EventRating, EventRatingPrediction>()
                .FromFile(modelName: "EventRecommenderModel", filePath: modelPath, watchForChanges: true);

            builder.Services.AddCors(options => {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                  policy => {
                      policy.WithOrigins("http://localhost:7235", "http://localhost:3000", "http://192.168.0.109:3000", "https://195.150.9.37", "https://91.216.191.181", "https://91.216.191.182", "https://91.216.191.183", "https://91.216.191.184", "https://91.216.191.185", "https://5.252.202.254", "https://5.252.202.255" , "http://192.168.30.2:3000")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(origin => true)
                .SetIsOriginAllowedToAllowWildcardSubdomains();
                  });
            });

            builder.Services.AddControllers();

            builder.Services.AddDbContext<DataContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var tokenOptions = builder.Configuration
              .GetSection(TokenOptions.CONFIG_NAME)
              .Get<TokenOptions>();
            builder.Services.Configure<TokenOptions>(
              builder.Configuration.GetSection(TokenOptions.CONFIG_NAME)
            );

            builder.Services
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(
                o => {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromMinutes(1),
                        IgnoreTrailingSlashWhenValidatingAudience = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(tokenOptions.SigningKey)
                  ),
                        ValidateIssuerSigningKey = tokenOptions.ValidateSigningKey,
                        RequireExpirationTime = true,
                        RequireAudience = true,
                        RequireSignedTokens = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidAudience = tokenOptions.Audience,
                        ValidIssuer = tokenOptions.Issuer
                    };
                    o.SaveToken = true;
                }
              );

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ITrackUserMovesService, TrackUserMovesService>();
            builder.Services.AddScoped<IOrganiserApplicationService, OrganiserApplicationService>();
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IEventsService, EventsService>();
            builder.Services.AddScoped<ITypesOfEventsService, TypesOfEventsService>();
            builder.Services.AddScoped<ILocationsService, LocationsService>();
            builder.Services.AddScoped<IUserEventHistoryService, UserEventHistoryService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddHostedService<ModelTrainerService>();
            builder.Services.AddHostedService<EventsCheckerService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}