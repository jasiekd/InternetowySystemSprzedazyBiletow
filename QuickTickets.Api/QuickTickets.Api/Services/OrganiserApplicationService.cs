using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace QuickTickets.Api.Services
{
    public class OrganiserApplicationService : IOrganiserApplicationService
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
                var data = _context.OrganisersApplications.AsQueryable().Include(x => x.User).Where(e => e.Status == StatusEnum.Pending.ToString());


                var totalCount = await data.CountAsync();
                var totalPages = (int)Math.Ceiling(totalCount / (double)paginationDto.pageSize);

                data = data.Skip((paginationDto.pageIndex - 1) * paginationDto.pageSize).Take(paginationDto.pageSize);

                List<OrganiserInfoDto> listOfOrganiserApps = new List<OrganiserInfoDto>();

                foreach( var temp in await data.ToListAsync())
                {
                    listOfOrganiserApps.Add(new OrganiserInfoDto
                    {
                        Id = temp.Id,
                        User = new UserInfoDto()
                        {
                            Name = temp.User.Name,
                            Surname = temp.User.Surname,
                            Email = temp.User.Email,
                            Login = temp.User.Login,
                            DateOfBirth = (DateTime)temp.User.DateOfBirth,
                        },
                        DateCreated = temp.DateCreated,
                        Status = temp.Status,
                        Description = temp.Description
                    });
                }

                var result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PageIndex = paginationDto.pageIndex,
                    PageSize = paginationDto.pageSize,
                    OrganiserApplications = listOfOrganiserApps
                };

                return new OkObjectResult(result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> SendOrganiserApplication([FromBody] OrganiserApplicationDto organiserApp)
        {
            var application = new OrganiserApplicationEntity
            {
                Id = 0,
                UserId = organiserApp.UserId,
                Description = organiserApp.Description,
            };
            _context.OrganisersApplications.Add(application);
            await _context.SaveChangesAsync();

            return new OkResult();
        }


        public async Task<IActionResult> AcceptOrganiserApplication([FromBody] long id)
        {

            OrganiserApplicationEntity organiserApplication = await _context.OrganisersApplications.FindAsync(id);
            AccountEntity accountEntity = await _context.Accounts.FindAsync(organiserApplication.UserId);

            if (organiserApplication == null)
            {
                return new NotFoundResult();
            }
            organiserApplication.Status = StatusEnum.Confirmed.ToString();
            organiserApplication.DateModified = DateTime.Now;
            accountEntity.RoleID = 3;

            _context.OrganisersApplications.Update(organiserApplication);
            _context.Accounts.Update(accountEntity);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<IActionResult> CancelOrganiserApplication([FromBody] long id)
        {
            OrganiserApplicationEntity organiserApplication = await _context.OrganisersApplications.FindAsync(id);
            AccountEntity accountEntity = await _context.Accounts.FindAsync(organiserApplication.UserId);

            if (organiserApplication == null)
            {
                return new NotFoundResult();
            }
            organiserApplication.Status = StatusEnum.Cancelled.ToString();
            organiserApplication.DateModified = DateTime.Now;
            accountEntity.RoleID = 3;

            _context.OrganisersApplications.Update(organiserApplication);
            _context.Accounts.Update(accountEntity);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

    }
}
