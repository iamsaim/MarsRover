using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.Infrastructure.Extension
{
    public static class ConfigureMapper
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services, Type assembly)
        {
            return services.AddAutoMapper(assembly);
        }
    }
}