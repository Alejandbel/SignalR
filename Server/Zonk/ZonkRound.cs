using Server.Excpetions;
using System;
using System.Numerics;

namespace Server.Zonk
{
	public class ZonkRound
	{
		static private int scoreToEnd = 300;
		static private readonly Random random = new();

		private record struct PlayerMove(string Name, ISet<int>? Dices = null, bool IsEnded = false, int Score = 0);

		static readonly Dictionary<int, int[]> scores = new Dictionary<int, int[]>
		{
			{1, new int[]{100, 200, 1000, 2000, 3000, 4000}},
			{2, new int[]{0, 0, 200, 400, 600, 800}},
			{3, new int[]{0, 0, 300, 600, 900, 1200}},
			{4, new int[]{0, 0, 400, 800, 1200, 1600}},
			{5, new int[]{50, 100, 500, 1000, 1500, 2000}},
			{6, new int[]{0, 0, 600, 1200, 1800, 2400}}
		};

		private Dictionary<string, PlayerMove> playersMove { get; set; } = new();


		public ZonkRound(IEnumerable<string> players)
		{
            foreach (var player in players)
            {
				playersMove.Add("string", new(player));
            }
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

			var score = GetScore(dices);

            foreach (var dice in dices)
            {
				player.Dices.Remove(dice);
            }
        }

		public void EndMove(string name)
		{
			var player = GetPlayerOrFail(name);


			if (player.Score < scoreToEnd)
			{
				throw new ServerException($"Score must be above {scoreToEnd} to end move");
			}

			player.IsEnded = true;
		}


		public IEnumerable<int> RollDices(string name)
		{
			var player = GetPlayerOrFail(name);

			
			if (player.Dices != null)
			{
				throw new ServerException("Dices already roled");
			}
			var dices = RollDices();
			player.Dices = dices as HashSet<int>;

			return dices;
		}

		private PlayerMove GetPlayerOrFail(string name)
		{
			PlayerMove player;
			if (!playersMove.TryGetValue(name, out player))
			{
				throw new ServerException("User not found in game");
			}

			if (player.IsEnded)
			{
				throw new ServerException($"Move already ended");
			}

			return player;
		}


		static private IEnumerable<int> RollDices()
		{
			for (int i = 0; i < 6; i++)
			{
				yield return random.Next(1, 7);
			}
		}

		static int GetScore(IEnumerable<int> dice)
		{
			var diceCounts = dice.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
			int diceLen = diceCounts.Count;

			if (diceLen == 6)
			{
				return 1000;
			}

			if (diceLen == 3 && diceCounts.All(x => x.Value == 2))
			{
				return 750;
			}

			return diceCounts.Sum(pair => scores[pair.Key][pair.Value - 1]);
		}
	}
}
