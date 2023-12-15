using Domain.Shared;
using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
	public class ZonkHub : Hub
	{
		public async Task<string> Send(string message)
		{
			await Clients.All.SendAsync("Receive", message);
			return "test";
		}
	}
}
