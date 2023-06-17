using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Entities;
using System.Transactions;
public class TransactionStatusUpdateService : BackgroundService
{
    private readonly DataContext _context;

    public TransactionStatusUpdateService(DataContext context)
    {
        _context = context;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Sprawdź transakcje, które przekroczyły termin płatności
            var overdueTransactions = await _context.Transactions.Where(x => x.DateDeadline >= DateTime.Now && x.Status == StatusEnum.Pending.ToString()).ToListAsync();

            foreach (var transaction in overdueTransactions)
            {
                transaction.Status = StatusEnum.Cancelled.ToString();
                transaction.DateUpdated = DateTime.Now;
                _context.Update(transaction);
                await _context.SaveChangesAsync();
            }


            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}