using System.Diagnostics.CodeAnalysis;

using infrastructure.Agents;
using infrastructure.Factory;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace infrastructure
{
    [Experimental("SEMX")]
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSemanticKernel(configuration).
                AddTransient<IVehicleAgent, VehicleAgent>().
                AddTransient<IModelContextPrtocolFactory, ModelContextPrtocolFactory>();
            return services;
        }
        public static IServiceCollection AddSemanticKernel(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddTransient<Kernel>(serviceProvider =>
            {
                IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
                kernelBuilder.Services.AddAzureOpenAIChatCompletion("gpt-4.1",
                  configuration["foundry-endpoint"],
                  configuration["apikey"],
                   "gpt-4.1",
                   "gpt-4.1");
                return kernelBuilder.Build();
            });
        }
    }
}
