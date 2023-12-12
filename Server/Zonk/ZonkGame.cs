namespace Server
{
	public class ZonkGame
	{
		private readonly int numberOfDices = 5;

		static readonly Dictionary<int, int[]> scores = new Dictionary<int, int[]>
		{
			{1, new int[]{100, 200, 1000, 2000, 3000, 4000}},
			{2, new int[]{0, 0, 200, 400, 600, 800}},
			{3, new int[]{0, 0, 300, 600, 900, 1200}},
			{4, new int[]{0, 0, 400, 800, 1200, 1600}},
			{5, new int[]{50, 100, 500, 1000, 1500, 2000}},
			{6, new int[]{0, 0, 600, 1200, 1800, 2400}}
		};

		private HashSet<string> players = new();
		private readonly Random random = new();

		public void AddPlayer(string name)
		{
			players.Add(name);
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
