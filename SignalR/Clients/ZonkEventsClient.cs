using Domain.Shared;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR.Clients
{
	public class ZonkEventsClient : IZonkEvents
	{
		public ZonkEventsClient(HubConnection connection)
		{
			connection.On<GameInfo>("GameChanged", game => { 
				GameHasUpdated?.Invoke(game);
			});
		}

		public event Action<GameInfo>? GameHasUpdated;
	}
}
