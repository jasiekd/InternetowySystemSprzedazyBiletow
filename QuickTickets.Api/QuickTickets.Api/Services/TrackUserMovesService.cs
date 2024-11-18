using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ML;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{
    public class TrackUserMovesService : ITrackUserMovesService
    {

        private readonly DataContext _context;
        public TrackUserMovesService(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Add(AddUserEventHistoryRequestDto addUserEventHistoryRequestDto, Guid userId)
        {
            if (_context.UserEventHistory == null)
            {
                return new NotFoundResult();
            }

            AccountEntity accountEntity = await _context.Accounts.FindAsync(userId);

            bool exists = await _context.UserEventHistory.AnyAsync(ueh =>
            ueh.Label == addUserEventHistoryRequestDto.Label &&
            ueh.EventID == addUserEventHistoryRequestDto.EventID &&
            ueh.UserID == int.Parse(accountEntity.ModelID));

            if (exists)
            {
                return new ConflictResult();
            }

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

    }
}