using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace GM.CommonLibs.Extensions
{
    public class APIKeyMessageHandlerMiddleware
    {
        private const string APIKeyToCheck = "repo2022";

        private RequestDelegate next;

        public APIKeyMessageHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.HasValue && 
                context.Request.Path.Value == "/" || context.Request.Path.Value == "/home")
            {
                await next.Invoke(context);
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
