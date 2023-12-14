using Domain;
using Server.Excpetions;

namespace Server.Zonk
{
	public class ZonkRound
	{
		static private int scoreToEnd = 300;

		private record struct PlayerMove(string Name, ISet<int>? Dices = null, int DicesLeft = 6, bool IsEnded = false, int Score = 0, bool IsZonked = false);

		private Dictionary<string, PlayerMove> playersMove { get; set; } = new();

		public ZonkRound(IEnumerable<string> players)
		{
            foreach (var player in players)
            {
				playersMove.Add("string", new(player));
            }
        }

		public int GetScore(string name)
		{
			var player = GetPlayerOrFail(name);
			return player.Score;
		}

		public void EndMove(string name)
		{
			var player = GetNotEndedPlayerOrFail(name);


			if (player.Score < scoreToEnd)
			{
				throw new ServerException($"Score must be above {scoreToEnd} to end move");
			}

			player.IsEnded = true;
		}

		public IEnumerable<int> RollDices(string name)
		{
			var player = GetNotEndedPlayerOrFail(name);

			if (player.DicesLeft == 0)
			{
				throw new ServerException("All dices already roled");
			}

			var dices = ZonkUtils.RollDices(player.DicesLeft);
			player.Dices = dices as HashSet<int>;

			if (!ZonkUtils.IsAbleToMove(dices))
			{
				player.IsZonked = true;
				player.Score = 0;
				return dices;
			} 

			return dices;
		}

		public void UpdateScore(string name, IEnumerable<int> dices)
		{
			var player = GetPlayerOrFail(name);

			if (player.Dices == null)
			{
				throw new ServerException("To update score you have to role dices first");
			}

			foreach (var dice in dices)
			{
				if (!player.Dices.Contains(dice))
				{
					throw new ServerException($"Invalid dice provided: {dice}");
				}
			}

			var score = ZonkUtils.GetScore(dices);

			foreach (var dice in dices)
			{
				player.Dices.Remove(dice);
			}

			player.Score += score;
			player.DicesLeft -= dices.Count();
		}

		private PlayerMove GetPlayerOrFail(string name)
		{
			PlayerMove player;
			if (!playersMove.TryGetValue(name, out player))
			{
				throw new ServerException("User not found in game");
			}

			return player;
		}

		private PlayerMove GetNotEndedPlayerOrFail(string name)
		{
			var player = GetPlayerOrFail(name);

			if (player.IsEnded)
			{
				throw new ServerException($"Move already ended");
			}

			return player;
		}
	}
}
