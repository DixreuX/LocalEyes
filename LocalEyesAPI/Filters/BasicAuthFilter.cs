using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace LocalEyesAPI.Filters
{
    public class BasicAuthFilter : IAuthorizationFilter
    {
        //      /*
        // * !!! IMPORTANT: Use this filter for READ-ONLY controllers !!!
        //*/

        //      private readonly IWebHostEnvironment _env;

        //      AppInfoHelper _appInfoHelper;

        //      public Root Config { get; private set; }
        //      public string ClientAuthCredentials { get; private set; }


        //      public ApiBasicAuthFilter(IWebHostEnvironment env)
        //      {
        //          _env = env;

        //          _appInfoHelper = new AppInfoHelper();
        //      }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            //Config = _appInfoHelper.GetConfig();
            //ClientAuthCredentials = Config.Config.Customer.ApiBasicAuthKey;

            //try
            //{

            //    //Montes:oTvv2Zk56#jHH_actWW] = "Basic TW9udGVzOm9UdnYyWms1NiNqSEhfYWN0V1dd"
            //    if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            //    {
            //        context.Result = new UnauthorizedObjectResult("Authorization is missing");
            //        return;
            //    }

            //    string authenticationHeader = authHeader;

            //    if (authenticationHeader.StartsWith("Basic "))
            //    {

            //        try
            //        {

            //            var encodedUsernamePassword = authenticationHeader.Substring("Basic ".Length).Trim();
            //            var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

            //            if (decodedUsernamePassword != ClientAuthCredentials)
            //            {
            //                context.Result = new UnauthorizedObjectResult("Invalid authorization header");
            //                return;
            //            }

            //        }
            //        catch
            //        {
            //            context.Result = new UnauthorizedObjectResult("Authorization validation error");
            //            return;
            //        }
            //    }

            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            //finally
            //{

            //}

            //app.Use(async (context, next) =>
            //{
            //    if (!context.Request.Headers.TryGetValue("APIKey", out var extractedApiKey) || extractedApiKey != builder.Configuration["APIKey"])
            //    {
            //        context.Response.StatusCode = 401; // Unauthorized
            //        await context.Response.WriteAsync("Invalid API Key");
            //        return;
            //    }

            //    await next();
            //});

        }
    }
}
