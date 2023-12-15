using Domain.Shared;

namespace Server.Hubs
{
	public interface IZonkHub
	{
		Task GameChanged(GameInfo game);
	}
}
