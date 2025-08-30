using Microsoft.AspNetCore.Mvc;
using Test_App.Models;

namespace Test_App.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly test_dbaseContext _context;

        // ✅ DbContext is injected here
        public UserController(test_dbaseContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            var str1 = "check changes";
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] Person person)
        {
            // Ignore PersonId if someone sends it
            person.PersonId = 0;

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return Ok(person);
        }

      
    }
}
