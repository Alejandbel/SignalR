namespace Domain.Shared
{
	public record GameInfo(
		string GameId, 
		IDictionary<string, List<int>> score, 
		int currentRound,
		IDictionary<string, IEnumerable<int>?> rolledDices,
		bool IsStarted
	);
}
