namespace service
{
    using infrastructure.Agents;
    public class VehicleService(IVehicleAgent vehicleAgent) : IVehicleService
    {
        public async Task<string> GetVehicleDetails(byte[] regitraionPlate)
        {
            return await vehicleAgent.Execute(regitraionPlate);
        }
    }

}
