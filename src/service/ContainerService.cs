namespace service
{
    using infrastructure.Agents;
    using infrastructure.Predictor;
    using infrastructure.Storage;
    using model;

    public class ContainerService(IContainerAgent containerAgent, IDamagePredictor damagePredictor, IBlobStorageService blobStorageService) : IContainerService
    {
        public async Task<(ContainerModel,string)> GetContainerDamage(byte[] containerImage)
        {
            var imageId = Guid.NewGuid().ToString();
            byte[] processedImage = await damagePredictor.GetDamage(containerImage);
            if (processedImage is null)
            {
                return (new ContainerModel
                {
                    Damage = new AgentResponse { Damages = new[] { new Damage { DamageDescription = "No Damage Detected" } } },
                    DamageImage = containerImage
                },imageId);
            }
            var agentReply = await containerAgent.Execute(processedImage);
           
            var model= new ContainerModel
            {
                Damage = agentReply,
                DamageImage = processedImage
            };
          
            await SaveAsync(model,imageId);
            return (model,imageId);
        }
        
        private async Task SaveAsync(ContainerModel model, string imageId)
        {
            await blobStorageService.UploadImageAsync(model.DamageImage, imageId);
        }
    }

}
