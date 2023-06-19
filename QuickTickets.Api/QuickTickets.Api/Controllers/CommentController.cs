using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Services;

namespace QuickTickets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("GetComments")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComments([FromBody] PaginationDto paginationDto, long eventID)
        {
            return await _commentService.GetComments(paginationDto, eventID);
        }

        [HttpPost("AddComment")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody]CreateCommentDto createCommentDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");
            
            return await _commentService.AddComment(createCommentDto, userId);
        }
        
        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        [AdminAuthorize]
        public async Task<IActionResult> DeleteCommentEntity(long id)
        {

            return await _commentService.DeleteCommentEntity(id);
        }

        
    }
}
