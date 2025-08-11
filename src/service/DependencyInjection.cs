using Microsoft.Extensions.DependencyInjection;

namespace service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IVehicleService, VehicleService>();
            return services;
        }
    }
}
