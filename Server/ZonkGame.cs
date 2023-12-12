namespace Server
{
	public class ZonkGame
	{
		private readonly int targetScore;
		private readonly int numberOfDices = 5;
		private Dictionary<string, int> PlayerScores { get; set; }
		private Random random;

		public ZonkGame(int targetScore)
		{
			this.targetScore = targetScore;
			PlayerScores = new Dictionary<string, int>();
			random = new Random();
		}

		public void AddPlayer(string playerName)
		{
			if (!PlayerScores.ContainsKey(playerName))
			{
				PlayerScoresP.Add(playerName, 0);
			}
		}

		public void PlayRound(string playerName)
		{
			var roundScore = RollDices();
			Console.WriteLine($"{playerName} rolled: {roundScore}");

			if (roundScore == 0)
			{
				Console.WriteLine($"{playerName} zonked! Round score is reset.");
				PlayerScores[playerName] = 0;
			}
			else
			{
				PlayerScores[playerName] += roundScore;
				Console.WriteLine($"{playerName} current score: {PlayerScores[playerName]}");

				if (PlayerScores[playerName] >= targetScore)
				{
					Console.WriteLine($"{playerName} wins!");
					ResetGame();
				}
			}
		}


		private IEnumerable<int> RollDices()
		{
			for (int i = 0; i < numberOfDices; i++)
			{
				yield return random.Next(1, 7);
			}
		}

		private void ResetGame()
		{
			foreach (var player in PlayerScores.Keys.ToList())
			{
				PlayerScores[player] = 0;
			}
		}
	}
}
