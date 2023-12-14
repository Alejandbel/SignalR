namespace Server
{
	public class ZonkGame
	{
		private HashSet<string> players = new();

		public void AddPlayer(string name)
		{
			players.Add(name);
		}
	}
}
