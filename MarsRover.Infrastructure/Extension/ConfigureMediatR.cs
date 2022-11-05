using MarsRover.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.Infrastructure.Extension
{
    public static class ConfigureMediatR
    {
        public static IServiceCollection AddAndConfigureMediatR(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("MarsRover.Application");
            services.AddMediatR(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }
}