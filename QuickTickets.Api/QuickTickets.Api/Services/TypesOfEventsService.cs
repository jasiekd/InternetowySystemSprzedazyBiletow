using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Data;
using QuickTickets.Api.Entities;
using Microsoft.EntityFrameworkCore;


namespace QuickTickets.Api.Services
{
    public class TypesOfEventsService : ITypesOfEventsService
    {
        private readonly DataContext _context;
        public TypesOfEventsService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<TypesOfEventsEntity>>> GetTypesOfEvents()
        {
            if (_context.TypesOfEvents == null)
            {
                return new NotFoundResult();
            }
            return await _context.TypesOfEvents.OrderByDescending(x => x.Description).ToListAsync();
        }

        public async Task<ActionResult<TypesOfEventsEntity>> PostTypesOfEventsEntity(TypesOfEventsEntity typesOfEventsEntity)
        {
            if (_context.TypesOfEvents == null)
            {
                return new NotFoundResult();
            }

            _context.TypesOfEvents.Add(typesOfEventsEntity);
            await _context.SaveChangesAsync();

            return new OkObjectResult(typesOfEventsEntity);
        }

    }
}
