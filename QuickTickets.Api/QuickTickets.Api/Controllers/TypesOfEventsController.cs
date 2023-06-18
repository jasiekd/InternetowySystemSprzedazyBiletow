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
    public class TypesOfEventsController : ControllerBase
    {
        private readonly DataContext _context;

        public TypesOfEventsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TypesOfEvents
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TypesOfEventsEntity>>> GetTypesOfEvents()
        {
          if (_context.TypesOfEvents == null)
          {
              return NotFound();
          }
            return await _context.TypesOfEvents.ToListAsync();
        }

        [HttpPost("AddTypeOfEvent")]
        [AdminAuthorize]
        public async Task<ActionResult<TypesOfEventsEntity>> PostTypesOfEventsEntity(TypesOfEventsEntity typesOfEventsEntity)
        {
          if (_context.TypesOfEvents == null)
          {
              return Problem("Entity set 'DataContext.TypesOfEvents'  is null.");
          }
            _context.TypesOfEvents.Add(typesOfEventsEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostTypesOfEventsEntity", new { id = typesOfEventsEntity.TypeID }, typesOfEventsEntity);
        }

    }
}
