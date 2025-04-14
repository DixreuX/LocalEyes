using LocalEyesAPI.Data;
using LocalEyesAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LocalEyesAPI.Filters
{
    public class JwtAuthFilter : IAuthorizationFilter
    {
        private readonly LocalEyesDbContext _context;
        private readonly IConfiguration _configuration;

        public JwtSecurityTokenHandler TokenHandler { get; private set; }

        public JwtAuthFilter(LocalEyesDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            TokenHandler = new JwtSecurityTokenHandler();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var url = context.HttpContext.Request.Path.Value;

            if (TokenHandler.CanReadToken(token))
            {
                var jwtToken = TokenHandler.ReadToken(token) as JwtSecurityToken;

                var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "email");
                var roleClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role");

                if (emailClaim != null && roleClaim != null)
                {
                    var email = emailClaim.Value;
                    var role = roleClaim.Value;

                    var user = _context.Users.FirstOrDefault(u => u.Email == email);

                    if (user == null || role != "API User")
                    {
                        context.Result = new UnauthorizedResult();

                        return;
                    }
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();

                return;
            }

            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            try
            {
                TokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "DannyF",
                    ValidAudience = "API User",
                    IssuerSigningKey = new SymmetricSecurityKey(key)

                }, out SecurityToken validatedToken);
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
