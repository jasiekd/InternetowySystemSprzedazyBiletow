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
        private readonly ILocationsService _locationsService;

        public LocationsController(ILocationsService locationsService)
        {
            _locationsService = locationsService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<LocationsEntity>>> GetLocations()
        {
          return await _locationsService.GetLocations();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<LocationsEntity>> GetLocationsEntity(long id)
        {
          return await _locationsService.GetLocationsEntity(id);
        }

        [HttpPost("addLocation")]
        [AdminAuthorize]
        public async Task<ActionResult<LocationsEntity>> PostLocationsEntity(LocationsEntity locationsEntity)
        {
          return await _locationsService.PostLocationsEntity(locationsEntity);
        }

    }
}
