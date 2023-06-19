using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickTickets.Api.Services
{
    public interface ITypesOfEventsService
    {
        Task<ActionResult<IEnumerable<TypesOfEventsEntity>>> GetTypesOfEvents();
        Task<ActionResult<TypesOfEventsEntity>> PostTypesOfEventsEntity(TypesOfEventsEntity typesOfEventsEntity);
    }
}