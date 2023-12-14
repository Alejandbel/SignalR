using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
	public class ZonkHub : Hub
	{
		public async Task Send(string message)
		{
			await Clients.All.SendAsync("Receive", message);
		}
	}
}
