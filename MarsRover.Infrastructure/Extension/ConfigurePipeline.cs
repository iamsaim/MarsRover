using MarsRover.Application.Common.Middleware;
using Microsoft.AspNetCore.Builder;

namespace MarsRover.Infrastructure.Extension
{
    public static class ConfigurePipeline
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            //app.UseMiddleware<UnhandledExceptionMiddleware>();
        }
    }
}