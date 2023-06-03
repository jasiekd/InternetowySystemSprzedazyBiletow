using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{
    public class OrganiserApplicationService
    { 
            private readonly DataContext _context;
            public OrganiserApplicationService(DataContext context)
            {
                _context = context;
            }
        public async Task<IActionResult> GetPendingOrganiserApplications(PaginationDto paginationDto)
        {
            try
            {
                var data = _context.OrganisersApplications.AsQueryable().Where(e => e.Status == StatusEnum.Pending.ToString());


                var totalCount = await data.CountAsync();
                var totalPages = (int)Math.Ceiling(totalCount / (double)paginationDto.pageSize);

                data = data.Skip((paginationDto.pageIndex - 1) * paginationDto.pageSize).Take(paginationDto.pageSize);

                var applications = await data.ToListAsync();

                var result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PageIndex = paginationDto.pageIndex,
                    PageSize = paginationDto.pageSize,
                    OrganiserApplications = applications
                };

                return new OkObjectResult(result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
