namespace infrastructure.Agents
{
    using System.Threading.Tasks;

    public interface IContainerAgent
    {
        Task<string> Execute(byte[] containerImage);
    }
}
