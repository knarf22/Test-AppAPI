using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Test_App.Models;
using Test_App.Models.DTO;
using Test_App.Models.Entities;

namespace Test_App.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly TestDbaseContext _context;

        // ✅ DbContext is injected here
        public UserController(TestDbaseContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            //var str1 = "check changes";
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            if (user == null)
                return NotFound(new { Message = "User not found" });

            // TODO: Replace with real password hashing check
            if (user.PasswordHash != request.Password)
                return Unauthorized(new { Message = "Invalid password" });

            // Return only safe data (DTO)
            var response = new UserLoginResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Token = "dummy-jwt-token" // later replace with real JWT
            };

            return Ok(response);
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest(new { Message = "Passwords do not match." });
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (existingUser != null)
            {
                return Conflict(new { Message = "Username already exists." });
            }

            // Create a placeholder Person with nulls
            var person = new Person
            {
                FirstName = null,
                LastName = null,
                MiddleName = null,
                City = null,
                Age = null
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            var user = new User
            {
                Username = request.Username,
                PasswordHash = request.Password, // ⚠️ TODO: hash this
                PersonId = person.PersonId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new UserRegisterResponse
            {
                UserId = user.UserId,
                Username = user.Username
            });
        }


    }
}
