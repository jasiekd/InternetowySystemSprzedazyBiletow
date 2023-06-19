using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{
    public class CommentService : ICommentService
    {
        private readonly IAccountService _accountService;
        private readonly DataContext _context;
        public CommentService(IAccountService accountService, DataContext context)
        {
            _accountService = accountService;
            _context = context;
        }

        private bool CommentEntityExists(long id)
        {
            return (_context.Comments?.Any(e => e.CommentID == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> GetComments(PaginationDto paginationDto, long eventID)
        {
            if (_context.Comments == null)
            {
                return new NotFoundResult();
            }
            return await GetCommentsForEvent(paginationDto, eventID);
        }
        public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto, Guid userId)
        {
            if (_context.Comments == null)
            {
                return new NotFoundResult();
            }


            var commentEntity = new CommentEntity
            {
                CommentID = 0,
                Content = createCommentDto.Content,
                EventID = createCommentDto.EventID,
                UserID = userId,
            };

            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
        public async Task<IActionResult> DeleteCommentEntity(long id)
        {
            if (_context.Comments == null)
            {
                return new NotFoundResult();
            }
            if (!CommentEntityExists(id))
            {
                return new NotFoundResult();
            }
            var commentEntity = await _context.Comments.FindAsync(id);


            _context.Comments.Remove(commentEntity);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<IActionResult> GetCommentsForEvent(PaginationDto paginationDto, long eventID)
        {
            try
            {
                var data = _context.Comments.AsQueryable().Include(e => e.User).Where(e => e.EventID == eventID).OrderByDescending(x => x.DateCreated);


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
