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
    public class CommentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly CommentService _commentService;

        public CommentController(DataContext context, CommentService commentService)
        {
            _context = context;
            _commentService = commentService;
        }

        // GET: api/Comment
        [HttpPost("GetComments")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComments([FromBody] PaginationDto paginationDto, long eventID)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return await _commentService.GetCommentsForEvent(paginationDto, eventID);
        }


        // POST: api/Comment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddComment")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody]CreateCommentDto createCommentDto)
        {
          if (_context.Comments == null)
          {
                return NotFound();
          }
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("BB47EEDE-6953-43DF-A26F-CDAC99BE8E87");
            var commentEntity = new CommentEntity
            {
                CommentID = 0,
                Content = createCommentDto.Content,
                EventID = createCommentDto.EventID,
                UserID = userId,
            };

            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }
        
        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        [AdminAuthorize]
        public async Task<IActionResult> DeleteCommentEntity(long id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            if (!CommentEntityExists(id))
            {
                return NotFound();
            }
            var commentEntity = await _context.Comments.FindAsync(id);
            

            _context.Comments.Remove(commentEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentEntityExists(long id)
        {
            return (_context.Comments?.Any(e => e.CommentID == id)).GetValueOrDefault();
        }
    }
}
