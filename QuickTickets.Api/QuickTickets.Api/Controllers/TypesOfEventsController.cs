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
    public class TypesOfEventsController : ControllerBase
    {
        private readonly DataContext _context;

        public TypesOfEventsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TypesOfEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypesOfEventsEntity>>> GetTypesOfEvents()
        {
          if (_context.TypesOfEvents == null)
          {
              return NotFound();
          }
            return await _context.TypesOfEvents.ToListAsync();
        }

        // GET: api/TypesOfEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypesOfEventsEntity>> GetTypesOfEventsEntity(long id)
        {
          if (_context.TypesOfEvents == null)
          {
              return NotFound();
          }
            var typesOfEventsEntity = await _context.TypesOfEvents.FindAsync(id);

            if (typesOfEventsEntity == null)
            {
                return NotFound();
            }

            return typesOfEventsEntity;
        }

        // PUT: api/TypesOfEvents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypesOfEventsEntity(long id, TypesOfEventsEntity typesOfEventsEntity)
        {
            if (id != typesOfEventsEntity.TypeID)
            {
                return BadRequest();
            }

            _context.Entry(typesOfEventsEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypesOfEventsEntityExists(id))
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

        // POST: api/TypesOfEvents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddTypeOfEvent")]
        public async Task<ActionResult<TypesOfEventsEntity>> PostTypesOfEventsEntity(TypesOfEventsEntity typesOfEventsEntity)
        {
          if (_context.TypesOfEvents == null)
          {
              return Problem("Entity set 'DataContext.TypesOfEvents'  is null.");
          }
            _context.TypesOfEvents.Add(typesOfEventsEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypesOfEventsEntity", new { id = typesOfEventsEntity.TypeID }, typesOfEventsEntity);
        }

        // DELETE: api/TypesOfEvents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypesOfEventsEntity(long id)
        {
            if (_context.TypesOfEvents == null)
            {
                return NotFound();
            }
            var typesOfEventsEntity = await _context.TypesOfEvents.FindAsync(id);
            if (typesOfEventsEntity == null)
            {
                return NotFound();
            }

            _context.TypesOfEvents.Remove(typesOfEventsEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypesOfEventsEntityExists(long id)
        {
            return (_context.TypesOfEvents?.Any(e => e.TypeID == id)).GetValueOrDefault();
        }
    }
}
