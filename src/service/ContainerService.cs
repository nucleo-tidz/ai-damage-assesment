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
            if(processedImage is null )
            {
                return new ContainerModel
                {
                    Analysis = "No damage detected",
                    DamageImage = containerImage
                };
            }
            string agentReply= await containerAgent.Execute(processedImage);
            return new ContainerModel
            {
                Analysis = agentReply,
                DamageImage = processedImage
            };
        }
    }

}
