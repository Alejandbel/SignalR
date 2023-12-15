using Domain;
using Server.Excpetions;
using Server.Zonk;
using System.Numerics;

namespace Server
{
	public class ZonkGame
	{
		protected HashSet<string> players;
		protected List<ZonkRound> rounds;
		protected bool isStarted = false;

		public string Id { get; }

		public ZonkGame(string id, string initializer)
		{
			players = new() { initializer };
			rounds = new();
			Id = id;
		}

		public void AddPlayer(string name)
		{
			if (isStarted)
			{
				throw new ServerException("Game already started");
			}

			players.Add(name);
		}

		public void StartGame()
		{
			isStarted = true;
			rounds.Add(new ZonkRound(players));
		}

		public void EndMove(string name)
		{
			var round = GetLastRound();
			round.EndMove(name);
		}

		public IEnumerable<int> RollDices(string name)
		{
			var round = GetLastRound();
			return round.RollDices(name);
		}

		public void UpdateScore(string name, IEnumerable<int> dices)
		{
			var round = GetLastRound();
			round.UpdateScore(name, dices);
		}

		public KeyValuePair<string, List<int>> GetScoreByPlayer(string name)
		{
			return new KeyValuePair<string, List<int>>(name, rounds.Select((round) => round.GetScore(name)).ToList());
		}

		public IDictionary<string, List<int>> GetScore()
		{
			return new Dictionary<string, List<int>>(players.Select(GetScoreByPlayer));
		}

		private ZonkRound GetLastRound()
		{
			return rounds.Last();
		}
	}
}
