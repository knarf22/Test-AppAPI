using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_App.Models;
using Test_App.Models.DTO;
using Test_App.Models.Entities;

namespace Test_App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly TestDbaseContext _context;

        public CommentsController(TestDbaseContext context)
        {
            _context = context;
        }

        // GET: api/comments/post/5
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsForPost(int postId)
        {
            var comments = await _context.Comments
                .Where(c => c.PostId == postId)
                .Select(c => new CommentDTO
                {
                    CommentId = c.CommentId,
                    PostId = c.PostId,
                    UserId = c.UserId,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();

            return Ok(comments);
        }

        // POST: api/comments
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> CreateComment(CreateCommentDTO dto)
        {
            var comment = new Comment
            {
                PostId = dto.PostId,
                UserId = dto.UserId,
                Text = dto.Text,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCommentsForPost), new { postId = comment.PostId }, comment);
        }

        // PUT: api/comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, UpdateCommentDto dto)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            comment.Text = dto.Text;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
