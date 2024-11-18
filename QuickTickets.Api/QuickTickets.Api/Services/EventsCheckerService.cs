using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuickTickets.Api.Data;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{
    public class EventsCheckerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public EventsCheckerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                        var outdatedEvents = context.Events
                            .Where(e => e.IsActive == true
                                        && e.Status == StatusEnum.Confirmed.ToString()
                                        && e.Date < DateTime.Now);

                        foreach (var eventItem in outdatedEvents)
                        {
                            eventItem.IsActive = false;
                        }

                        await context.SaveChangesAsync(stoppingToken);
                        Console.WriteLine($"Usunieto {outdatedEvents.Count()} wydarzen!");
                    }

                    Console.WriteLine($"Sprawdzenie wydarzeń zostało zakonczone o: {DateTime.Now}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd podczas sprawdzania aktywnych wydarzeń: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
