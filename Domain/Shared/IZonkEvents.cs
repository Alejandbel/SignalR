namespace Domain.Shared
{
	public interface IZonkEvents
	{
		event Action<GameInfo> GameHasUpdated;
	}
}
