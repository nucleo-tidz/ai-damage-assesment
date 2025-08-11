
using vehicle.mcp.server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMcpServer().WithHttpTransport(option => { option.Stateless = true; }).WithTools<VehicleTool>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
app.UseHttpsRedirection();
app.MapMcp();
app.Run();