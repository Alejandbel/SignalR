using Domain;
using Domain.Shared;
using Server.Excpetions;
using Server.Zonk;
using System.Numerics;

namespace Server
{
	public class ZonkGame
	{
		protected static int roundsToEnd = 10;

		protected HashSet<string> players;
		protected List<ZonkRound> rounds;
		protected bool isStarted = false;
		protected bool isEnded = false;
		protected string? winner;

		public string Id { get; }
		public GameInfo GameInfo { get => new GameInfo(
			Id, 
			GetScore(),
			GetLastRoundNumber(),
			(rounds != null && rounds.Count() > 0) ? GetLastRound().PlayersMove : null, 
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
			AssertGameNotStarted();
			players.Add(name);
		}

		public void StartGame()
		{
			AssertGameNotEnded();
			AssertGameNotStarted();

			isStarted = true;
			rounds.Add(new ZonkRound(players));
		}

		public void EndMove(string name, IEnumerable<int> dices)
		{
			AssertGameNotEnded();
			var round = GetLastRound();
			round.EndMove(name, dices);
			EndRoundIfPossible(round);
		}

		public IEnumerable<int> RollDices(string name)
		{
			AssertGameNotEnded();
			var round = GetLastRound();
			return round.RollDices(name);
		}

		public IEnumerable<int> RerollDices(string name, IEnumerable<int> dices)
		{
			AssertGameNotEnded();
			var round = GetLastRound();
			var newDices = round.RerollDices(name, dices);
			EndRoundIfPossible(round);
			return newDices;
		}

		private void EndRoundIfPossible(ZonkRound round)
		{
			if (rounds.Count() == roundsToEnd)
			{
				isEnded = true;
				winner = GetScore().MaxBy(kv => kv.Value).Key;
			}

			if (round.IsEnded)
			{
				rounds.Add(new ZonkRound(players));
			}
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

		private void AssertGameNotEnded()
		{
			if (isEnded)
			{
				throw new ServerException("Game already ended");
			}
		}

		private void AssertGameNotStarted()
		{
			if (isStarted)
			{
				throw new ServerException("Game already started");
			}
		}

	}
}
