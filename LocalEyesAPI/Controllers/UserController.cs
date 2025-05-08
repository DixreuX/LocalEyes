using LocalEyes.Shared.Models;
using LocalEyesAPI.Data;
using LocalEyesAPI.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocalEyesAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LocalEyesDbContext _context;

        public UserController(UserManager<ApplicationUser> userManager, LocalEyesDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// Get all users | Basic Auth + Administrator role
        /// </summary>
        [HttpGet("Users")]
        [ServiceFilter<BasicAuthFilter>]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.Select(user => new
            {
                user.Id,
                user.UserName,
                user.Email
            }).ToList();

            return Ok(users);
        }

        /// <summary>
        /// Create a new user with the role "user" | Basic Auth + Administrator role
        /// </summary>
        [HttpPost("CreateUser")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> CreateUser([FromBody] NewUser newUser)
        {
            var user = new ApplicationUser
            {
                UserName = newUser.Email,
                Email = newUser.Email
            };

            var result = await _userManager.CreateAsync(user, newUser.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, "User");

            var municipalityUser = new MunicipalityUser
            {
                Id = Guid.NewGuid(),
                MunicipalityId = newUser.MunicipalityId,
                UserId = user.Id
            };

            _context.MunicipalityUsers.Add(municipalityUser);

            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Create a new user with the role "APIUser" | Basic Auth + Administrator role
        /// </summary>
        [HttpPost("CreateAPIUser")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> CreateAPIUser([FromBody] NewAPIUser newAPIUser)
        {
            var user = new ApplicationUser
            {
                UserName = newAPIUser.Email,
                Email = newAPIUser.Email
            };

            var result = await _userManager.CreateAsync(user, newAPIUser.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, "API User");

            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Delete a user by ID | Basic Auth + Administrator role
        /// </summary>
        [HttpDelete("Delete/{id:guid}")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
