namespace infrastructure.Agents
{
    using model;
    using System.Threading.Tasks;

    public interface IContainerAgent
    {
        Task<AgentResponse> Execute(byte[] containerImage);
    }
}
