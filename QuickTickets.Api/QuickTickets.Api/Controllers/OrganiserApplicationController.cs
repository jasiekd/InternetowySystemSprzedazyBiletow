using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;

namespace QuickTickets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganiserApplicationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly OrganiserApplicationService _organiserApplicationService;

        public OrganiserApplicationController(DataContext context, OrganiserApplicationService organiserApplicationService)
        {
            _context = context;
            _organiserApplicationService = organiserApplicationService;
        }



        [HttpPost("SendOrganiserApplication")]
        public async Task<IActionResult> SendOrganiserApplication([FromBody]OrganiserApplicationDto organiserApp)
        {
            var application = new OrganiserApplicationEntity
            {
                Id = 0,
                UserId = organiserApp.UserId,
                Description = organiserApp.Description,
            };
            _context.OrganisersApplications.Add(application);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("GetPendingOrganiserApplications")]
        public async Task<IActionResult> GetPendingOrganiserApplications([FromBody] PaginationDto paginationDto)
        {
            var result = await _organiserApplicationService.GetPendingOrganiserApplications(paginationDto);
            return Ok(result);
        }


        [HttpPost("AcceptOrganiserApplication")]
        public async Task<IActionResult> AcceptOrganiserApplication([FromBody]long id)
        {

            OrganiserApplicationEntity organiserApplication = await _context.OrganisersApplications.FindAsync(id);
            AccountEntity accountEntity = await _context.Accounts.FindAsync(organiserApplication.UserId);

            if (organiserApplication == null)
            {
                return NotFound();
            }
            organiserApplication.Status = StatusEnum.Confirmed.ToString();
            organiserApplication.DateModified = DateTime.Now;
            accountEntity.RoleID = 3;
            
            _context.OrganisersApplications.Update(organiserApplication);
            _context.Accounts.Update(accountEntity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("CancelOrganiserApplication")]
        public async Task<IActionResult> CancelOrganiserApplication([FromBody] long id)
        {

            OrganiserApplicationEntity organiserApplication = await _context.OrganisersApplications.FindAsync(id);

            if (organiserApplication == null)
            {
                return NotFound();
            }
            organiserApplication.Status = StatusEnum.Cancelled.ToString();
            organiserApplication.DateModified = DateTime.Now;

            _context.OrganisersApplications.Update(organiserApplication);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: api/OrganiserApplication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganiserApplicationEntity>>> GetOrganisersApplications()
        {
          if (_context.OrganisersApplications == null)
          {
              return NotFound();
          }
            return await _context.OrganisersApplications.ToListAsync();
        }

        // GET: api/OrganiserApplication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganiserApplicationEntity>> GetOrganiserApplicationEntity(long id)
        {
          if (_context.OrganisersApplications == null)
          {
              return NotFound();
          }
            var organiserApplicationEntity = await _context.OrganisersApplications.FindAsync(id);

            if (organiserApplicationEntity == null)
            {
                return NotFound();
            }

            return organiserApplicationEntity;
        }

        // PUT: api/OrganiserApplication/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganiserApplicationEntity(long id, OrganiserApplicationEntity organiserApplicationEntity)
        {
            if (id != organiserApplicationEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(organiserApplicationEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganiserApplicationEntityExists(id))
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

        // POST: api/OrganiserApplication
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrganiserApplicationEntity>> PostOrganiserApplicationEntity(OrganiserApplicationEntity organiserApplicationEntity)
        {
          if (_context.OrganisersApplications == null)
          {
              return Problem("Entity set 'DataContext.OrganisersApplications'  is null.");
          }
            _context.OrganisersApplications.Add(organiserApplicationEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganiserApplicationEntity", new { id = organiserApplicationEntity.Id }, organiserApplicationEntity);
        }

        // DELETE: api/OrganiserApplication/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganiserApplicationEntity(long id)
        {
            if (_context.OrganisersApplications == null)
            {
                return NotFound();
            }
            var organiserApplicationEntity = await _context.OrganisersApplications.FindAsync(id);
            if (organiserApplicationEntity == null)
            {
                return NotFound();
            }

            _context.OrganisersApplications.Remove(organiserApplicationEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganiserApplicationEntityExists(long id)
        {
            return (_context.OrganisersApplications?.Any(e => e.Id == id)).GetValueOrDefault();
        }







    }
}
