using Microsoft.AspNetCore.Mvc;
using Notepad.Api.Data;
using Notepad.Api.Models;
using Auth.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace Notepad.api.Controllers
{
    [Route("api/[controller]")] // This attribute defines the route for the controller. The [controller] token will be replaced with the name of the controller, which is "Auth" in this case.
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")] // This attribute specifies that this action will handle POST requests to the "register" endpoint (e.g., /api/auth/register).
        public async Task<IActionResult> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Registration SuccessFully" });
        }

        [HttpPost("login")] // This attribute specifies that this action will handle POST requests to the "login" endpoint (e.g., /api/auth/login).
        public async Task<IActionResult> Login([FromBody] User loginInfo)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginInfo.Email && u.Password == loginInfo.Password);

            if (user == null) return Unauthorized("Invalid Email or Password");

            return Ok(new { userId = user.Id, email = user.Email, role = user.Role });
        }

        [HttpGet("total-users")] // This attribute specifies that this action will handle GET requests to the "users" endpoint (e.g., /api/auth/users).
        public async Task<IActionResult> GetAllUsers()
        {
            var count = await _context.Users.CountAsync();
            return Ok(new { total = count });
            //return Ok(new { message = "Logout SuccessFully" });
        }

    }
}
