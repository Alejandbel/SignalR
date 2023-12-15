using Domain;
using Domain.Shared;
using Server.Excpetions;
using Server.Zonk;
using System.Numerics;

namespace Server
{
	public class ZonkGame
	{
		protected HashSet<string> players;
		protected List<ZonkRound> rounds;
		private bool isStarted = false;

		public string Id { get; }
		public GameInfo GameInfo { get => new GameInfo(
			Id, 
			GetScore(),
			GetLastRoundNumber(),
			(rounds == null || rounds.Count() == 0) ? GetLastRound().PlayersMove : null, 
			isStarted);
		}

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

		public void EndMove(string name, IEnumerable<int> dices)
		{
			var round = GetLastRound();
			round.EndMove(name, dices);
		}

		public IEnumerable<int> RollDices(string name)
		{
			var round = GetLastRound();
			return round.RollDices(name);
		}

		public IEnumerable<int> RerollDices(string name, IEnumerable<int> dices)
		{
			var round = GetLastRound();
			return round.RerollDices(name, dices);
		}


		public ZonkRound GetLastRound()
		{
			return rounds.Last();
		}

		public int GetLastRoundNumber()
		{
			return rounds.Count() - 1;
		}

		public KeyValuePair<string, List<int>> GetScoreByPlayer(string name)
		{
			return new KeyValuePair<string, List<int>>(name, rounds.Select((round) => round.GetScore(name)).ToList());
		}

		public IDictionary<string, List<int>> GetScore()
		{
			return new Dictionary<string, List<int>>(players.Select(GetScoreByPlayer));
		}

	}
}
