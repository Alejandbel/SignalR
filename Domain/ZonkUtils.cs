using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public static class ZonkUtils
	{
		private static Random random = new Random();

		public static bool IsAbleToMove(IEnumerable<int> dices)
		{
			var score = GetScore(dices);
			return score > 0;
		}

		public static IEnumerable<int> RollDices(int amount)
		{
			List<int> result = new List<int>();
			for (int i = 0; i < amount; i++)
			{
				result.Add(random.Next(1, 7));
			}
			return result;
		}

		public static bool MoveCanBeMade(IEnumerable<int> dices)
		{
			if (dices.Count() == 0)
			{
				return false;
			} 

			var diceCounts = dices.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
			int diceLen = diceCounts.Count;

			if (diceLen == 6)
			{
				return true;
			}

			if (diceLen == 3 && diceCounts.All(x => x.Value == 2))
			{
				return true;
			}

			foreach (var kvp in diceCounts)
			{
				int count = kvp.Value;
				int number = kvp.Key;

				switch (number)
				{
					case 2:
					case 3:
					case 4:
					case 6:
						if (count != 3)
							return false;
						break;
				}
			}

			return true;
		}

		public static int GetScore(IEnumerable<int> dices)
		{
			var diceCounts = dices.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
			int diceLen = diceCounts.Count;
			
			if (diceLen == 6)
			{
				return 3000;
			}

			if (diceLen == 3 && diceCounts.All(x => x.Value == 2))
			{
				return 1500;
			}

			int totalScore = 0;

			foreach (var kvp in diceCounts)
			{
				int count = kvp.Value;
				int number = kvp.Key;

				switch (number)
				{
					case 1:
						totalScore += count >= 3 ? 1000 + (count - 3) * 100 : count * 100;
						break;
					case 5:
						totalScore += count >= 3 ? 500 + (count - 3) * 50 : count * 50;
						break;
					default:
						if (count >= 3)
							totalScore += number * 100;
						break;
				}
			}

			return totalScore;
		}
	}
}
