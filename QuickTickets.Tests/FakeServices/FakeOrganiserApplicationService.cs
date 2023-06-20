using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;
using QuickTickets.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTickets.Tests.FakeServices
{
    public class FakeOrganiserApplicationService : IOrganiserApplicationService
    {
        List<OrganiserApplicationEntity> _organiserApplications;
        public FakeOrganiserApplicationService() 
        { 
            _organiserApplications = new List<OrganiserApplicationEntity>()
            {
                new OrganiserApplicationEntity()
                {
                    Id = 1,
                    UserId = Guid.NewGuid(),
                    Description = "Fajny wniosek",
                },
                new OrganiserApplicationEntity()
                {
                    Id = 2,
                    UserId = Guid.NewGuid(),
                    Description = "Fajny wniosek v2",
                },
                new OrganiserApplicationEntity()
                {
                    Id = 3,
                    UserId = Guid.NewGuid(),
                    Description = "Fajny wniosek v3",
                },
                new OrganiserApplicationEntity()
                {
                    Id = 4,
                    UserId = Guid.NewGuid(),
                    Description = "Fajny wniosek v4",
                }
            };
        }
        public async Task<IActionResult> SendOrganiserApplication([FromBody] OrganiserApplicationDto organiserApp)
        {
            _organiserApplications.Add(new OrganiserApplicationEntity
            {
                Id = 0,
                UserId = organiserApp.UserId,
                Description = organiserApp.Description,
            });
            return new OkResult();
        }
        public async Task<IActionResult> GetPendingOrganiserApplications([FromBody] PaginationDto paginationDto)
        {
            var data = _organiserApplications.Where(e => e.Status == StatusEnum.Pending.ToString()).OrderByDescending(x => x.DateCreated);


            var totalCount = data.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)paginationDto.pageSize);

            data = (IOrderedEnumerable<OrganiserApplicationEntity>)data.Skip((paginationDto.pageIndex - 1) * paginationDto.pageSize).Take(paginationDto.pageSize);

            List<OrganiserInfoDto> listOfOrganiserApps = new List<OrganiserInfoDto>();

            foreach (var temp in data)
            {
                listOfOrganiserApps.Add(new OrganiserInfoDto
                {
                    Id = temp.Id,
                    User = new UserInfoDto()
                    {
                        Name = null,
                        Surname = null,
                        Email = null,
                        Login = null,
                        DateOfBirth = null,
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
            

        public async Task<IActionResult> AcceptOrganiserApplication([FromBody] long id)
        {
            throw new NotImplementedException();
        }
        public async Task<IActionResult> CancelOrganiserApplication([FromBody] long id)
        {
            throw new NotImplementedException();
        }
    }
}
