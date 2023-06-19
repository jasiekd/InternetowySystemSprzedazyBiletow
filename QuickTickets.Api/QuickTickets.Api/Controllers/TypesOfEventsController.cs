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
        private readonly ITypesOfEventsService _typesOfEventsService;

        public TypesOfEventsController(ITypesOfEventsService typesOfEventsService)
        {
            _typesOfEventsService = typesOfEventsService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TypesOfEventsEntity>>> GetTypesOfEvents()
        {

            return await _typesOfEventsService.GetTypesOfEvents();
        }

        [HttpPost("AddTypeOfEvent")]
        [AdminAuthorize]
        public async Task<ActionResult<TypesOfEventsEntity>> PostTypesOfEventsEntity(TypesOfEventsEntity typesOfEventsEntity)
        {
            return await _typesOfEventsService.PostTypesOfEventsEntity(typesOfEventsEntity);
        }

    }
}
