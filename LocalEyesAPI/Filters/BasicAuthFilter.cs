using LocalEyesAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace LocalEyesAPI.Filters
{
    public class BasicAuthFilter : IAuthorizationFilter
    {

        private readonly EncryptionHelper _securityHelper;

        public BasicAuthFilter(EncryptionHelper securityHelper)
        {
            _securityHelper = securityHelper;
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
            var encryptedKey = _securityHelper.EncryptKey();

            if (encryptedKey != providedKey)
            {
                context.Result = new UnauthorizedObjectResult("Invalid APIKey");

                return;
            }

            // If the keys match, the request is authorized

        }
    }
}
