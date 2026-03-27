using Azure.AI.Agents.Persistent;
using Azure.AI.Projects;
using Azure.Identity;

using infrastructure.Agents;
using infrastructure.Predictor;
using infrastructure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using System.Diagnostics.CodeAnalysis;

namespace infrastructure
{
    [Experimental("SEMX")]
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSemanticKernel(configuration).
                AddTransient<IContainerAgent, ContainerAgent>()
                  .AddTransient<IDamagePredictor, DamagePredictor>()
                  .AddTransient<IBlobStorageService, BlobStorageService>()
            .Configure<CustomVisionSettings>(configuration.GetSection("CustomVision"));
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
