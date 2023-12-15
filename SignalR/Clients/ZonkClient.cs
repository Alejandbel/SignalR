using Domain.Shared;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR.Clients
{
	public class ZonkClient : IZonkClient
	{
        private readonly HubConnection _connection;

        public ZonkClient(HubConnection connection)
        {
            _connection = connection;
        }
        public Task<ResponseDTO> AddPlayer(string gameId, string name)
		{
			return _connection.InvokeAsync<ResponseDTO>("AddPlayer", gameId, name);
		}

		public Task<ResponseDTO> EndMove(string gameId, string name, IEnumerable<int> dices)
		{
            return _connection.InvokeAsync<ResponseDTO>("EndMove", gameId, name, dices);
        }

        public Task<ResponseDTO<GameInfo>> GetGame(string gameId)
        {
            return _connection.InvokeAsync<ResponseDTO<GameInfo>>("GetGame", gameId);
        }

		public Task<ResponseDTO<IEnumerable<GameInfo>>> GetGames()
		{
            return _connection.InvokeAsync<ResponseDTO<IEnumerable<GameInfo>>>("GetGames");
        }

        public Task<ResponseDTO<GameInfo>> InitializeGame(string name)
		{
            return _connection.InvokeAsync<ResponseDTO<GameInfo>>("InitializeGame", name);
            ;
        }

        public Task<ResponseDTO<IEnumerable<int>>> RerollDices(string gameId, string name, IEnumerable<int> dices)
		{
			return _connection.InvokeAsync<ResponseDTO<IEnumerable<int>>>("RerollDices", gameId, name, dices);
		}

		public Task<ResponseDTO<IEnumerable<int>>> RollDices(string gameId, string name)
		{
            return _connection.InvokeAsync<ResponseDTO<IEnumerable<int>>>("RollDices", gameId, name);
        }

        public Task<ResponseDTO> StartGame(string gameId)
		{
            return _connection.InvokeAsync<ResponseDTO>("StartGame", gameId);
        }
    }
}
