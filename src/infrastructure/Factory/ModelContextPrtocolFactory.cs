namespace infrastructure.Factory
{
    using System;
    using System.Threading.Tasks;

    using ModelContextProtocol.Client;

    public class ModelContextPrtocolFactory : IModelContextPrtocolFactory
    {
        public async Task<IMcpClient> Create()
        {
            var clientTransport = new SseClientTransport(
                    new SseClientTransportOptions
                    {
                        Endpoint = new Uri("https://localhost:7265"),
                        TransportMode = HttpTransportMode.StreamableHttp
                    }
                );
            return await McpClientFactory.CreateAsync(clientTransport);
        }
    }
}
