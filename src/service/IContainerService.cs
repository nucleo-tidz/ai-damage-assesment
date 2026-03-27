namespace service
{
    using model;
    using System.Threading.Tasks;

    public interface IContainerService
    {
        Task<(ContainerModel, string)> GetContainerDamage(byte[] containerImage);
    }
}
