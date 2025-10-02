using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_App.Models;
using Test_App.Models.DTO;
using Test_App.Models.Entities;

namespace Test_App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly TestDbaseContext _context;

        public PostsController(TestDbaseContext context)
        {
            _context = context;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts()
        {
            var posts = await _context.Posts
                .Include(p => p.Comments)
                .Select(p => new PostDTO
                {
                    PostId = p.PostId,
                    UserId = p.UserId,
                    Title = p.Title,
                    Body = p.Body,
                    CreatedAt = p.CreatedAt,
                    Comments = p.Comments.Select(c => new CommentDTO
                    {
                        CommentId = c.CommentId,
                        PostId = c.PostId,
                        UserId = c.UserId,
                        Text = c.Text,
                        CreatedAt = c.CreatedAt
                    }).ToList()
                })
                .ToListAsync();

            return Ok(posts);
        }

        // GET: api/posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null) return NotFound();

            var dto = new PostDTO
            {
                PostId = post.PostId,
                UserId = post.UserId,
                Title = post.Title,
                Body = post.Body,
                CreatedAt = post.CreatedAt,
                Comments = post.Comments.Select(c => new CommentDTO
                {
                    CommentId = c.CommentId,
                    PostId = c.PostId,
                    UserId = c.UserId,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };

            return Ok(dto);
        }

        // POST: api/posts
        [HttpPost]
        public async Task<ActionResult<PostDTO>> CreatePost(CreatePostDTO dto)
        {
            var post = new Post
            {
                UserId = dto.UserId,
                Title = dto.Title,
                Body = dto.Body,
                CreatedAt = DateTime.UtcNow
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.PostId }, post);
        }

        // PUT: api/posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, UpdatePostDto dto)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            post.Title = dto.Title;
            post.Body = dto.Body;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
