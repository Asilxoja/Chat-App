using DataAccsesLayer;
using DataAccsesLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace ChatApp.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController(AppDbContext dbContext) : ControllerBase
{
    private readonly AppDbContext _dbContext = dbContext;

    [HttpPost("register")]
    public  async Task<IActionResult> Register([FromBody] RegisterUser register)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == register.PhoneNumber);
        if (user != null)
        {
            return BadRequest("User aready exists");
        }

        User newUser = new User
        {
            FullName = register.FullName,
            PhoneNumber = register.PhoneNumber,
            Password = register.Password,
        };

        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();
        return Ok("Added");
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUser login) 
    {

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == login.PhoneNumber);
        if (user == null)
        {
            return BadRequest("Invalid User");
        }

        if (user.Password != login.Password)
        {
            return BadRequest("Invalid credentitals");
        }

        return Ok("Ok");
    }
}
