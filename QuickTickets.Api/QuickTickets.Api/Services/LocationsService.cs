using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly DataContext _context;

        public LocationsService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<LocationsEntity>>> GetLocations()
        {
            if (_context.Locations == null)
            {
                return new NotFoundResult();
            }
            return await _context.Locations.OrderByDescending(x => x.Name).ToListAsync();
        }
        public async Task<ActionResult<LocationsEntity>> GetLocationsEntity(long id)
        {
            if (_context.Locations == null)
            {
                return new NotFoundResult();
            }
            var locationsEntity = await _context.Locations.FindAsync(id);

            if (locationsEntity == null)
            {
                return new NotFoundResult();
            }

            return locationsEntity;
        }
        public async Task<ActionResult<LocationsEntity>> PostLocationsEntity(LocationsEntity locationsEntity)
        {
            if (_context.Locations == null)
            {
                return new NotFoundResult();
            }
            _context.Locations.Add(locationsEntity);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}
