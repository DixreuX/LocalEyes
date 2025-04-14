using LocalEyesAPI.Data;
using LocalEyesAPI.Helpers;
using LocalEyesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocalEyesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthorizationController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel model)
        {
            TokenGenerator tokenGenerator = new TokenGenerator(_userManager, _configuration);

            // Check if user is null 
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Unauthorized("Invalid user");
            }

            // Check if role is valid
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("API User"))
            {
                return Unauthorized("invalid user");
            }

            // Check if password is valid
            bool validPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!validPassword)
            {
                return Unauthorized("Invalid credentials");
            }

            // Check if key is valid
            var keyFromLoginModel = model.Key;
            var keyFromConfig = _configuration["JWT:Key"];

            if (keyFromLoginModel != keyFromConfig)
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate JWT - If all security conditions are met   
            var token = tokenGenerator.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
