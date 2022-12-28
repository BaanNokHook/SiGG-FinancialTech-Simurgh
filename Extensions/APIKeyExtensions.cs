using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace GM.CommonLibs.Extensions
{
    public static class APIKeyExtensions
    {

        public static IApplicationBuilder UseAPIKeyMessageHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APIKeyMessageHandlerMiddleware>();
        }

        public static IApplicationBuilder UseAuthExtensionsHandlerMiddleware(this IApplicationBuilder builder,IConfiguration Configuration)
        {
            return builder.UseMiddleware<AuthExtensionsHandlerMiddleware>(Configuration);
        }
    }
}