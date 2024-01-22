using DataAccessLayer.Models;
using DataAccsesLayer;
using DataAccsesLayer.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ChatApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(AppDbContext dbContext) : ControllerBase
    {
        private readonly AppDbContext _dbContext = dbContext;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser register)
        {
            var user = await _dbContext.Users.Find(u => u.PhoneNumber == register.PhoneNumber).FirstOrDefaultAsync();
            if (user != null)
            {
                return BadRequest("User already exists");
            }

            User newUser = new()
            {
                FullName = register.FullName,
                PhoneNumber = register.PhoneNumber,
                Password = register.Password,
            };

           await _dbContext.Users.InsertOneAsync(newUser);
            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser login)
        {
            var user = await _dbContext.Users.Find(u => u.PhoneNumber == login.PhoneNumber).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("Invalid User");
            }

            if (user.Password != login.Password)
            {
                return BadRequest("Invalid credentials");
            }

            return Ok("Login successful");
        }
    }
}
