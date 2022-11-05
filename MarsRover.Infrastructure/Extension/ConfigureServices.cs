using MarsRover.Application.Common.Contracts;
using MarsRover.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.Infrastructure.Extension
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IPlateauService, PlateauService>();
            services.AddScoped<IMarsRoverService, MarsRoverService>();

            return services;
        }
    }
}
