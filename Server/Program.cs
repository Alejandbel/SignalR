using Microsoft.AspNetCore.Http.Connections;
using Server.Hubs;

namespace Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			builder.Services.AddSignalR();

			var app = builder.Build();

			app.MapHub<ZonkHub>("/zonk", o => o.Transports = HttpTransportType.WebSockets);
			app.Run();
		}
	}
}