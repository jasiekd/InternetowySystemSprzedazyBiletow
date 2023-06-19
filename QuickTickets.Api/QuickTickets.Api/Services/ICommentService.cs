using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Dto;

namespace QuickTickets.Api.Services
{
    public interface ICommentService
    {
        public Task<IActionResult> GetComments(PaginationDto paginationDto, long eventID);
        public Task<IActionResult> AddComment(CreateCommentDto createCommentDto, Guid userId);
        public Task<IActionResult> DeleteCommentEntity(long id);
    }
}
