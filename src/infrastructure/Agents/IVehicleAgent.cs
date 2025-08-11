namespace infrastructure.Agents
{
    using System.Threading.Tasks;

    public interface IVehicleAgent
    {
        Task<string> Execute(byte[] regitraionPlate);
    }
}
