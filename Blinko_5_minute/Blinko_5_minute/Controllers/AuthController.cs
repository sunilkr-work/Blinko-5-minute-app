using Blinko_5_minute.model;
using Blinko_5_minute.service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blinko_5_minute.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthController(AuthenticationService authenticationService, IPasswordHasher<User> hash)
        {
            _authenticationService = authenticationService;
            _passwordHasher = hash;

        }

        [HttpPost("register-user")]
        public async Task<ActionResult> RegisterUser([FromBody] User user)
        {
            await _authenticationService.RegisterUser(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] User user)
        {
            var loginUser = await _authenticationService.TryToGetUser(user.Id);
            if (loginUser == null) { return BadRequest("invalid user"); };

            var result = _passwordHasher.VerifyHashedPassword(loginUser, loginUser.PasswordHash, user.PasswordHash);
            if(result != PasswordVerificationResult.Success) 
                {
                    return Unauthorized();
                }
            var token = _authenticationService.GenerateJwtToken(loginUser);
            return Ok(new { token });
        }
    }
}
