namespace infrastructure.Agents
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using infrastructure.Factory;

    using Microsoft.Extensions.Configuration;
    using Microsoft.SemanticKernel;
    using Microsoft.SemanticKernel.Agents;
    using Microsoft.SemanticKernel.Agents.AzureAI;
    using Microsoft.SemanticKernel.ChatCompletion;

    using ModelContextProtocol.Client;
    internal class VehicleAgent(Kernel _kernel, IConfiguration configuration, IModelContextPrtocolFactory clientFactory) : AgentBase(_kernel, configuration), IVehicleAgent
    {

        public async Task<string> Execute(byte[] regitraionPlate)
        {
            var mcpClient = await clientFactory.Create();
            var mcpTools = await mcpClient.ListToolsAsync();
            string agentReply = string.Empty;
            var agent = base.GetAzureAgent(configuration["VehicleAgentId"]);
            agent.Item1.Kernel.Plugins.AddFromFunctions("VehicleTool", mcpTools.Select(_ => _.AsKernelFunction()));
            AgentThread thread = new AzureAIAgentThread(agent.Item2);
            ChatMessageContentItemCollection messages = new ChatMessageContentItemCollection();
            // messages.Add(new TextContent("Get registration number from the image"));
            messages.Add(new ImageContent(regitraionPlate, "image/jpeg"));
            ChatMessageContent chatMessageContent = new(AuthorRole.User, messages);
            await foreach (ChatMessageContent response in agent.Item1.InvokeAsync(chatMessageContent, thread))
            {
                agentReply = agentReply + response.Content;
            }
            return agentReply;
        }
    }
}
