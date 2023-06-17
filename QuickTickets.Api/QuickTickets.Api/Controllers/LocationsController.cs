using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;

namespace QuickTickets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly DataContext _context;

        public LocationsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Locations
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<LocationsEntity>>> GetLocations()
        {
          if (_context.Locations == null)
          {
              return NotFound();
          }
            return await _context.Locations.ToListAsync();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<LocationsEntity>> GetLocationsEntity(long id)
        {
          if (_context.Locations == null)
          {
              return NotFound();
          }
            var locationsEntity = await _context.Locations.FindAsync(id);

            if (locationsEntity == null)
            {
                return NotFound();
            }

            return locationsEntity;
        }

   

        // POST: api/Locations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("addLocation")]
        [AdminAuthorize]
        public async Task<ActionResult<LocationsEntity>> PostLocationsEntity(LocationsEntity locationsEntity)
        {
          if (_context.Locations == null)
          {
              return Problem("Entity set 'DataContext.Locations'  is null.");
          }
            _context.Locations.Add(locationsEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocationsEntity", new { id = locationsEntity.LocationID }, locationsEntity);
        }

    }
}
