namespace infrastructure.Factory
{
    using System.Threading.Tasks;

    using ModelContextProtocol.Client;

    internal interface IModelContextPrtocolFactory
    {
        Task<IMcpClient> Create();
    }
}
