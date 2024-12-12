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
        private readonly ITicketService _ticketService;
        private readonly PredictionEnginePool<EventRating, EventRatingPrediction> _predictionEnginePool;
        public UserEventHistoryService(DataContext context, PredictionEnginePool<EventRating, EventRatingPrediction> predictionEnginePool, IEventsService eventsService, ITicketService ticketService)
        {
            _context = context;
            _predictionEnginePool = predictionEnginePool;
            _eventsService = eventsService;
            _ticketService = ticketService;
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

            var ticketsQuery = await _ticketService.GetTicketsForUserFromDb(userId, true);

            var ticketEventIds = ticketsQuery.Select(ticket => ticket.EventID).ToList();

            var topEvents = predictions
                            .Where(p => !ticketEventIds.Contains(p.EventId))         //nowe
                            .OrderByDescending(p => p.Score)  // Sortuj po najwyższym score
                            .Take(3)  // Weź 3 najlepsze
                            .ToList();

            //var topEvents = predictions.OrderByDescending(p => p.Score).Take(3);   //stare
            var ListOfEventsInfo = new List<EventInfoDto>();

            bool showingHotEvents = false;
            foreach (var eventEntity in topEvents) { 
                if(float.IsNaN(eventEntity.Score) || eventEntity.Score == 0)
                    showingHotEvents = true;
            }

            if (showingHotEvents) 
            {
                Console.WriteLine($"Hot events for user {ModelID}");
                var hotEvents = await _eventsService.getHotEventsInfo();
            }
            else
            {
                Console.WriteLine($"Top 3 recommended events for user {ModelID}:");
                foreach (var (eventId, score) in topEvents)
                {
                    Console.WriteLine($"EventId: {eventId}, Predicted Score: {score}");
                    ListOfEventsInfo.Add(_eventsService.GetEventInfoDto(eventsEntity.Find(x => x.EventID == eventId)));
                }
            }
            
            
            return new OkObjectResult(ListOfEventsInfo);
        }
    }
}
