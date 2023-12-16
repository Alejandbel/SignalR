using Microsoft.AspNetCore.Http.Connections;
using Server.Hubs;
using Server.Zonk;

namespace Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddSignalR();
			builder.Services.AddSingleton<ZonkRooms>();

			var app = builder.Build();

			app.MapHub<ZonkHub>("/zonk", o => o.Transports = HttpTransportType.WebSockets);
			app.Run();
		}
	}
}