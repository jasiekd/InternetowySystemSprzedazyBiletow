using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{
    public class CommentService
    {
        private readonly AccountService _accountService;
        private readonly DataContext _context;
        public CommentService(AccountService accountService, DataContext context)
        {
            _accountService = accountService;
            _context = context;
        }



        public async Task<IActionResult> GetCommentsForEvent(PaginationDto paginationDto, long eventID)
        {
            try
            {
                var data = _context.Comments.AsQueryable().Include(e => e.User).Where(e => e.EventID == eventID);


                return await GetPaginatedComments(paginationDto, data);

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private async Task<IActionResult> GetPaginatedComments(PaginationDto paginationDto, IQueryable<CommentEntity> data)
        {
            var totalCount = await data.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)paginationDto.pageSize);

            data = data.Skip((paginationDto.pageIndex - 1) * paginationDto.pageSize).Take(paginationDto.pageSize);

            var commentList = new List<dynamic>();

            foreach (var temp in await data.ToListAsync())
            {
                commentList.Add(new
                {
                    CommentID = temp.CommentID,
                    User = _accountService.GetUserInfoDto(temp.User),
                    DateCreated = temp.DateCreated,
                    Content = temp.Content,
                });
            }

            var result = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageIndex = paginationDto.pageIndex,
                PageSize = paginationDto.pageSize,
                Comments = commentList
            };
            return new OkObjectResult(result);
        }
    }
}
