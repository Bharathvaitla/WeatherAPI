using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Weather_server.JwtHelper;

namespace Weather_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            if (req == null)
                return BadRequest("Invalid request");
                //throw new ArgumentNullException(nameof(request));

            if (req.Username == "admin" && req.Password == "a123")
            {
                var token = JwtHelper.JwtHelper.GenerateToken(req.Username);
                return Ok(new
                {
                    token,
                });
            }
            return Unauthorized("Invalid username or password");
        }
    }

    // Custom Login DTO
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
