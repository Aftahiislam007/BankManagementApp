using BankManagement.Models;
using BankManagement.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateDto userDto)
        {
            var role = _context.Roles.SingleOrDefault(r => r.Name == userDto.Role);
            if (role == null)
            {
                return BadRequest("Invalid role");
            }     

            var user = new Users
            {
                Username = userDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                RoleId = role.Id
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserUpdateDto userDto)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            user.Username = userDto.Username;
            if (!string.IsNullOrEmpty(userDto.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpGet("admin")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult GetAdminData()
        {
            return Ok("This is admin-only data.");
        }

        [HttpGet("user")]
        [Authorize(Policy = "UserOnly")]
        public IActionResult GetUserData()
        {
            return Ok("This is user-only data.");
        }

    }
}
