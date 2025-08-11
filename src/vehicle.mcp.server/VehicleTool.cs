namespace vehicle.mcp.server
{
    using System.ComponentModel;

    using model;

    using ModelContextProtocol.Server;

    [McpServerToolType]
    public class VehicleTool
    {
        [McpServerTool, Description("Get the details of vehicle by its Registration number")]
        public VehicleModel GetVehichleDetail([Description("Registration number of the vehicle")] string registrationNumber)
        {
            return new VehicleModel
            {
                OwnerName = "Ahmar",
                Make = "Suzuki",
                Model = "Baleno",
                Year = 2017,
                Color = "Blue",
                VIN = "JTDBR32E720123456",
                RegistrationNumber = registrationNumber
            };
        }
    }
}
