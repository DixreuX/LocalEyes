using LocalEyes.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace LocalEyesAPI.Filters
{
    public class BasicAuthFilter : IAuthorizationFilter
    {

        private readonly EncryptionHelper _encryptionHelper;
        private readonly IConfiguration _configuration;

        private string _apiKey = "";

        public BasicAuthFilter(IConfiguration configuration)
        {

            _configuration = configuration;

            _apiKey = _configuration["BasicAuth:APIKey"];

            _encryptionHelper = new EncryptionHelper(_apiKey);
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            // Check if the APIKey header is present
            if (!context.HttpContext.Request.Headers.TryGetValue("APIKey", out var providedKey))
            {
                context.Result = new UnauthorizedObjectResult("APIKey is missing");

                return;
            }

            // Encrypt the provided key and compare it with the stored key
            var encryptedKey = _encryptionHelper.EncryptKey();

            if (encryptedKey != providedKey)
            {
                context.Result = new UnauthorizedObjectResult("Invalid APIKey");

                return;
            }

            // If the keys match, the request is authorized

        }
    }
}
