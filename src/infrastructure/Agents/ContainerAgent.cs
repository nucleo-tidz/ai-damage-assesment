namespace infrastructure.Agents
{
    using Azure;
    using Azure.AI.Agents.Persistent;

    using Microsoft.Extensions.Configuration;
    using Microsoft.SemanticKernel;
    using Microsoft.SemanticKernel.Agents;
    using Microsoft.SemanticKernel.Agents.AzureAI;
    using Microsoft.SemanticKernel.ChatCompletion;
    using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
    using model;
    using ModelContextProtocol.Protocol;
    using System.Text.Json;
    using System.Threading.Tasks;
    internal class ContainerAgent(Kernel _kernel, IConfiguration configuration) :  IContainerAgent
    {
        public async Task<AgentResponse> Execute(byte[] containerImage)
        {
            var settings = new AzureOpenAIPromptExecutionSettings()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
                ResponseFormat = typeof(AgentResponse)
                //MaxTokens=10

            };
            string agentReply = string.Empty;
            var argument = new KernelArguments(settings);
           var agent= new ChatCompletionAgent
            {
                Name = "ShippingContainerDamageDetectionAgent",
                Instructions = @"You are a specialized shipping container damage assessment expert. Your task is to analyze images of shipping containers and identify damages.

                       IMPORTANT: Focus ONLY on areas marked with red rectangles in the image. These rectangles highlight the specific damage areas that need assessment.
                       
                       For each red rectangle area, provide:
                       
                       1. **DamageType**: Classify the damage 
                       
                       2. **DamageDescription**: Provide a detailed description of:
                          - The exact nature and extent of the damage
                          - The location on the container (e.g., front panel, side wall, door, corner, etc.)
                          - The approximate size or severity
                          - Any visible characteristics (depth, width, affected surface area)
                       
                       3. **PotentialImplications**: List possible consequences, such as:
                          - Structural integrity concerns
                          - Water ingress risk
                          - Security vulnerabilities
                          - Cargo damage risk
                          - Compliance or certification issues
                       
                       4. **RecommendedActions**: Suggest appropriate responses, such as:
                          - Immediate repair requirements
                          - Inspection recommendations
                          - Usage restrictions
                          - Documentation needs
                          - Priority level (urgent, moderate, low)
                       
                       Focus exclusively on the damages within the red rectangles. Ignore any other areas of the container. Be precise, professional, and thorough in your assessment. Return your analysis in the specified JSON format.",
                Kernel = _kernel,
                Description = "A Shipment agent",
                Arguments = argument,

            };
            ChatHistory history = new ChatHistory();
            history.AddUserMessage([
             
                new ImageContent(containerImage, "image/jpeg")
            ]);
            await foreach (ChatMessageContent response in agent.InvokeAsync(history))
            {
                agentReply = agentReply + response.Content;
            }
            return JsonSerializer.Deserialize<AgentResponse>(agentReply);
        }
    }
}
