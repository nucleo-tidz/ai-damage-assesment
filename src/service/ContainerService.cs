namespace service
{
    using infrastructure.Agents;
    using infrastructure.Predictor;
    using model;

    public class ContainerService(IContainerAgent containerAgent, IDamagePredictor damagePredictor) : IContainerService
    {
        public async Task<ContainerModel> GetContainerDamage(byte[] containerImage)
        {
            byte[] processedImage = await damagePredictor.GetDamage(containerImage);
            if (processedImage is null)
            {
                return new ContainerModel
                {
                    Damage = new AgentResponse { Damages = new[] { new Damage { DamageDescription = "No Damage Detected" } } },
                    DamageImage = containerImage
                };
            }
            var agentReply = await containerAgent.Execute(processedImage);
            return new ContainerModel
            {
                Damage = agentReply,
                DamageImage = processedImage
            };
        }
    }

}
