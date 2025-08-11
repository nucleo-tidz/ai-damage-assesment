namespace infrastructure.Agents
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.SemanticKernel;
    using Microsoft.SemanticKernel.Agents;
    using Microsoft.SemanticKernel.Agents.AzureAI;
    using Microsoft.SemanticKernel.ChatCompletion;
    using ModelContextProtocol.Client;
    using System.Threading.Tasks;
    internal class ContainerAgent(Kernel _kernel, IConfiguration configuration) : AgentBase(_kernel, configuration), IContainerAgent
    {

        public async Task<string> Execute(byte[] containerImage)
        {
            string agentReply = string.Empty;
            var agent = base.GetAzureAgent(configuration["ContainerAgentId"]);
            AgentThread thread = new AzureAIAgentThread(agent.Item2);
            ChatMessageContentItemCollection messages = new ChatMessageContentItemCollection();
            messages.Add(new ImageContent(containerImage, "image/jpeg"));
            ChatMessageContent chatMessageContent = new(AuthorRole.User, messages);
            await foreach (ChatMessageContent response in agent.Item1.InvokeAsync(chatMessageContent, thread))
            {
                agentReply = agentReply + response.Content;
            }
            return agentReply;
        }
    }
}
