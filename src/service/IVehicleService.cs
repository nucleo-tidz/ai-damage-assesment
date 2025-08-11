namespace service
{
    using System.Threading.Tasks;

    public interface IVehicleService
    {
        Task<string> GetVehicleDetails(byte[] regitraionPlate);
    }
}
