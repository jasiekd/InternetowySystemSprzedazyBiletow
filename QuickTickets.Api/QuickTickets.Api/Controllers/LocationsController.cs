using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Entities;

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

        // PUT: api/Locations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationsEntity(long id, LocationsEntity locationsEntity)
        {
            if (id != locationsEntity.LocationID)
            {
                return BadRequest();
            }

            _context.Entry(locationsEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationsEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Locations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("addLocation")]
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

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationsEntity(long id)
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

            _context.Locations.Remove(locationsEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationsEntityExists(long id)
        {
            return (_context.Locations?.Any(e => e.LocationID == id)).GetValueOrDefault();
        }
    }
}
