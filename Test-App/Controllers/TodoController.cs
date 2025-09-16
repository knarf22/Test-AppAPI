using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_App.Models;
using Test_App.Models.DTO;
using Test_App.Models.Entities;

namespace Test_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TestDbaseContext _context;

        public TodoController(TestDbaseContext context)
        {
            _context = context;
        }

        // ✅ Create a new Todo
        [HttpPost]
        public async Task<ActionResult<TodoResponse>> CreateTodo([FromBody] TodoRequest request)
        {
            var todo = new Todo
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                IsDone = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            // Add history
            var history = new TodoHistory
            {
                TodoId = todo.TodoId,
                UserId = todo.UserId,
                Action = "Created",
                ActionDate = DateTime.UtcNow,
                NewTitle = todo.Title,
                NewDescription = todo.Description
            };

            _context.TodoHistories.Add(history);
            await _context.SaveChangesAsync();

            return Ok(new TodoResponse
            {
                TodoId = todo.TodoId,
                UserId = todo.UserId,
                Title = todo.Title,
                Description = todo.Description,
                IsDone = todo.IsDone,
                CreatedAt = todo.CreatedAt
            });
        }

        // ✅ Get all active (not deleted) Todos for a user
        [HttpGet("active/{userId}")]
        public async Task<ActionResult<IEnumerable<TodoResponse>>> GetActive(int userId)
        {
            var todos = await _context.Todos
                .Where(t => t.UserId == userId && t.DeletedAt == null)
                .Select(t => new TodoResponse
                {
                    TodoId = t.TodoId,
                    UserId = t.UserId,
                    Title = t.Title,
                    Description = t.Description,
                    IsDone = t.IsDone,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt

                })
                .ToListAsync();

            return Ok(todos);
        }

        // ✅ Mark a Todo as completed
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null || todo.DeletedAt != null) return NotFound();

            todo.IsDone = true;
            todo.UpdatedAt = DateTime.UtcNow;

            // Save history
            var history = new TodoHistory
            {
                TodoId = todo.TodoId,
                UserId = todo.UserId,
                Action = "Completed",
                ActionDate = DateTime.UtcNow
            };

            _context.TodoHistories.Add(history);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ Soft delete a Todo
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null || todo.DeletedAt != null) return NotFound();

            todo.DeletedAt = DateTime.UtcNow;

            // Save history
            var history = new TodoHistory
            {
                TodoId = todo.TodoId,
                UserId = todo.UserId,
                Action = "Deleted",
                ActionDate = DateTime.UtcNow,
                OldTitle = todo.Title,
                OldDescription = todo.Description
            };

            _context.TodoHistories.Add(history);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ Get Todo history
        [HttpGet("{id}/history")]
        public async Task<ActionResult<IEnumerable<TodoHistoryResponse>>> GetHistory(int id)
        {
            var histories = await _context.TodoHistories
                .Where(h => h.TodoId == id)
                .OrderByDescending(h => h.ActionDate)
                .Select(h => new TodoHistoryResponse
                {
                    HistoryId = h.HistoryId,
                    TodoId = h.TodoId,
                    UserId = h.UserId,
                    Action = h.Action,
                    ActionDate = h.ActionDate,
                    OldTitle = h.OldTitle,
                    NewTitle = h.NewTitle,
                    OldDescription = h.OldDescription,
                    NewDescription = h.NewDescription
                })
                .ToListAsync();

            return Ok(histories);
        }

        // ✅ Update/modify a Todo
        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoRequest request)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null || todo.DeletedAt != null) return NotFound();

            // Save old values for history
            var oldTitle = todo.Title;
            var oldDescription = todo.Description;

            // Update values
            todo.Title = request.Title;
            todo.Description = request.Description;
            todo.UpdatedAt = DateTime.UtcNow;

            // Save history
            var history = new TodoHistory
            {
                TodoId = todo.TodoId,
                UserId = todo.UserId,
                Action = "Updated",
                ActionDate = DateTime.UtcNow,
                OldTitle = oldTitle,
                OldDescription = oldDescription,
                NewTitle = todo.Title,
                NewDescription = todo.Description
            };

            _context.TodoHistories.Add(history);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
