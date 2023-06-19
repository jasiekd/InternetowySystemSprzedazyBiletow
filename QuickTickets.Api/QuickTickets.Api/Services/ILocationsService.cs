using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{
    public interface ILocationsService
    {
        Task<ActionResult<IEnumerable<LocationsEntity>>> GetLocations();
        Task<ActionResult<LocationsEntity>> GetLocationsEntity(long id);
        Task<ActionResult<LocationsEntity>> PostLocationsEntity(LocationsEntity locationsEntity);
    }
}
