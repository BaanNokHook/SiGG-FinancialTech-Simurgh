using GM.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GM.CommonLibs.Extensions
{
    public class AuthExtensionsHandlerMiddleware
    {
        private string APIKeyToCheck = "repo2022";

        private RequestDelegate next;
        IConfiguration Configuration;

        public AuthExtensionsHandlerMiddleware(RequestDelegate next, IConfiguration Configuration)
        {
            this.next = next;
            this.Configuration = Configuration;
        }

        public async Task Invoke(HttpContext context)
        {

            var listToken = Configuration.GetSection("TokenAuthentication").Get<List<TokenAuthentication>>();

            if (context.Request.Path.HasValue && 
                context.Request.Path.Value == ""
                || context.Request.Path.Value.ToUpper() == "/"
                || context.Request.Path.Value.ToUpper() == "/HOME/INDEX"
                || context.Request.Path.Value.ToUpper() == "/FAVICON.ICO")
            {
                await next.Invoke(context);
            }
            else
            {
                if (listToken.Exists(x => x.Path == context.Request.Path.Value))
                {
                    APIKeyToCheck = listToken.Find(x => x.Path == context.Request.Path.Value).SecretKey;
                }

                bool validKey = false;
                var checkApiKeyExists = context.Request.Headers.ContainsKey("x-access-token");
                if (checkApiKeyExists)
                {
                    if (context.Request.Headers["x-access-token"].Equals(APIKeyToCheck))
                    {
                        validKey = true;
                    }

                    if (!validKey)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await context.Response.WriteAsync("Invalid API Key");
                    }
                    else
                    {
                        await next.Invoke(context);
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync("No API Key");
                }
            }
        }
    }
}