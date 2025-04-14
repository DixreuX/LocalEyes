using LocalEyesAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace LocalEyesAPI.Helpers
{
    public class TokenGenerator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public JwtSecurityTokenHandler TokenHandler { get; private set; }

        public TokenGenerator(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;

            TokenHandler = new JwtSecurityTokenHandler();
        }

        public string GenerateToken(ApplicationUser applicationUser)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            // Create claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Iss, applicationUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim(ClaimTypes.Role, "API User")
            };

            // Customize token descripters 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = "DannyF",
                Audience = "API User",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = TokenHandler.CreateToken(tokenDescriptor);

            return TokenHandler.WriteToken(token);
        }
    }
}
