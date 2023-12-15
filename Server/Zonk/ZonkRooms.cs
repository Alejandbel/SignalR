using Domain.Shared;
using Server.Excpetions;

namespace Server.Zonk
{
	public class ZonkRooms
	{
		private IDictionary<string, ZonkGame> games = new Dictionary<string, ZonkGame>();

		public GameInfo AddGame(string initializer)
		{
			var uuid = Guid.NewGuid().ToString();
			games[uuid] = new ZonkGame(uuid, initializer);
			return games[uuid].GameInfo;
		}

		public IEnumerable<GameInfo> ListGames()
		{
			return games.Values.Select(game => game.GameInfo);
		}

		public GameInfo GetGameInfo(string gameId)
		{
			var game = GetGameOrFail(gameId);
			return game.GameInfo;
		}

		public ZonkGame GetGameOrFail(string gameId)
		{
			ZonkGame game;
			if (!games.TryGetValue(gameId, out game))
			{
				throw new ServerException("Game not found");
			};

			return game;
		}
	}
}
