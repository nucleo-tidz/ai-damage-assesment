namespace infrastructure.Agents
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.SemanticKernel;
    using Microsoft.SemanticKernel.Agents;
    using Microsoft.SemanticKernel.Agents.AzureAI;
    using Microsoft.SemanticKernel.ChatCompletion;
    using model;
    using System.Text.Json;
    using System.Threading.Tasks;
    internal class ContainerAgent(Kernel _kernel, IConfiguration configuration) : AgentBase(_kernel, configuration), IContainerAgent
    {
        public object responseFormat = new
        {
            type = "json_schema",
            json_schema = new
            {
                name = "DamageReport",
                schema = new
                {
                    type = "object",
                    properties = new
                    {
                        Damages = new
                        {
                            type = "array",
                            items = new
                            {
                                type = "object",
                                properties = new
                                {
                                    DamageType = new { type = "string" },
                                    DamageDescription = new { type = "string" },
                                    PotentialImplications = new { type = "array", items = new { type = "string" } },
                                    RecommendedActions = new { type = "array", items = new { type = "string" } }
                                },
                                required = new[] { "DamageType", "DamageDescription" }
                            }
                        }
                    },
                    required = new[] { "Damages" }
                }
            }
        };


        public async Task<AgentResponse> Execute(byte[] containerImage)
        {
            string agentReply = string.Empty;
            //base.UpdateAgent(configuration["ContainerAgentId"], responseFormat);
            var agent = base.GetAzureAgent(configuration["ContainerAgentId"]);
            AgentThread thread = new AzureAIAgentThread(agent.Item2);
            ChatMessageContentItemCollection messages = new();
            messages.Add(new ImageContent(containerImage, "image/jpeg"));
            ChatMessageContent chatMessageContent = new(AuthorRole.User, messages);
            await foreach (ChatMessageContent response in agent.Item1.InvokeAsync(chatMessageContent, thread))
            {
                agentReply = agentReply + response.Content;
            }
            return JsonSerializer.Deserialize<AgentResponse>(agentReply);
        }
    }
}
