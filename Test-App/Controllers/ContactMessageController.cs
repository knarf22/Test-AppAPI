using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_App.Models;
using Test_App.Models.DTO;
using Test_App.Models.Entities;

namespace Test_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMessagesController : ControllerBase
    {
        private readonly TestDbaseContext _context;

        public ContactMessagesController(TestDbaseContext context)
        {
            _context = context;
        }

        // GET: api/ContactMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactMessageDTO>>> GetContactMessages()
        {
            return await _context.ContactMessages
               .Select(cm => new ContactMessageDTO
               {
                   Id = cm.Id,
                   UserId = cm.UserId,
                   Name = cm.Name,
                   Email = cm.Email,
                   Message = cm.Message,
                   Status = cm.Status
               })
               .ToListAsync();
        }


        // POST: api/ContactMessages
        [HttpPost]
        public async Task<ActionResult<ContactMessageDTO>> PostContactMessage(ContactMessageDTO contactMessage)
        {
            // Make sure UserId is valid
            var user = await _context.Users.FindAsync(contactMessage.UserId);
            if (user == null)
            {
                return BadRequest("Invalid UserId");
            }

            var entity = new ContactMessage
            {
                UserId = user.UserId,
                Name= contactMessage.Name,
                Email = contactMessage.Email,
                Message = contactMessage.Message,
                Status = "Pending"
            };

            _context.ContactMessages.Add(entity);
            await _context.SaveChangesAsync();

            return Ok("Success");
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var entity = await _context.ContactMessages.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Status = status;

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
