using EyelashesAPI.Configuration;
using EyelashesAPI.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EyelashesAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Requests.LoginRequest request)
        {
            if (request.Username == AppConstants.Username 
                && request.Password == AppConstants.Password)
            {
                var token = _jwtService.GenerateToken(request.Username);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
}
