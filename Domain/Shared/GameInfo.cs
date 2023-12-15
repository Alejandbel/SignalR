namespace Domain.Shared
{
	public record GameInfo(
		string GameId, 
		IDictionary<string, List<int>> Score, 
		int CurrentRound,
		IDictionary<string, PlayerMove>? Moves,
		bool IsStarted
	);
}
