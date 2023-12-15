namespace Server.Zonk
{
	public class ZonkRooms
	{
		private IDictionary<string, ZonkGame> games = new Dictionary<string, ZonkGame>();

		public string AddGame(string initializer)
		{
			var uuid = Guid.NewGuid().ToString();
			games[uuid] = new ZonkGame(uuid, initializer);
			return uuid;
		}

		public string[] ListGames()
		{
			return games.Keys.ToArray();
		}
	}
}
