using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Server.Excpetions;
using Server.Zonk;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Server.Hubs
{
	public class ZonkHub : Hub<IZonkHub>, IZonkClient
	{
		private readonly ZonkRooms _zonkRooms;

		public ZonkHub([FromServices] ZonkRooms zonkGame)
		{
			_zonkRooms = zonkGame;
		}

		public Task<ResponseDTO<IEnumerable<GameInfo>>> GetGames()
		{
			ResponseDTO<IEnumerable<GameInfo>> result;
			try
			{
				var games = _zonkRooms.ListGames();
				result = new(games);
			}
			catch (ServerException err)
			{
				result = new(err);
			}
			return Task.FromResult(result);
		}

		public Task<ResponseDTO<GameInfo>> GetGame(string gameId)
		{
			ResponseDTO<GameInfo> result;
			try
			{
				var game = _zonkRooms.GetGameInfo(gameId);
				result = new(game);
			}
			catch (ServerException err)
			{
				result = new(err);
			}
			return Task.FromResult(result);
		}

		public async Task<ResponseDTO> AddPlayer(string gameId, string name)
		{
			ResponseDTO result;
			try
			{
				var game = _zonkRooms.GetGameOrFail(gameId);
				game.AddPlayer(name);
				await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
				result = new();
			}
			catch (ServerException err)
			{
				result = new(err);
			}
			return result;
		}

		public async Task<ResponseDTO> EndMove(string gameId, string name, IEnumerable<int> dices)
		{
			ResponseDTO result;
			try
			{
				var game = _zonkRooms.GetGameOrFail(gameId);
				game.EndMove(name, dices);
				await Clients.Groups(gameId).GameChanged(game.GameInfo);
				result = new();
			}
			catch (ServerException err)
			{
				result = new(err);
			}
			return result;
		}

		public async Task<ResponseDTO<GameInfo>> InitializeGame(string name)
		{
			ResponseDTO<GameInfo> result;
			try
			{
				var game = _zonkRooms.AddGame(name);
				await Groups.AddToGroupAsync(Context.ConnectionId, game.GameId);
				result = new(game);
			}
			catch (ServerException err)
			{
				result = new(err);
			}
			return result;
		}

		public async Task<ResponseDTO<IEnumerable<int>>> RerollDices(string gameId, string name, IEnumerable<int> dices)
		{
			ResponseDTO<IEnumerable<int>> result;
			try
			{
				var game = _zonkRooms.GetGameOrFail(gameId);
				var newDices = game.RerollDices(name, dices);
				await Clients.Groups(gameId).GameChanged(game.GameInfo);
				result = new(newDices);
			}
			catch (ServerException err) { result = new(err); }
			return result;
		}

		public Task<ResponseDTO<IEnumerable<int>>> RollDices(string gameId, string name)
		{
			ResponseDTO<IEnumerable<int>> result;
			try
			{
				var game = _zonkRooms.GetGameOrFail(gameId);
				var newDices = game.RollDices(name);
				result = new(newDices);
			}
			catch (ServerException err)
			{
				result = new(err);
			}
			return Task.FromResult(result);
		}

		public Task<ResponseDTO> StartGame(string gameId)
		{
			ResponseDTO result;
			try
			{
				var game = _zonkRooms.GetGameOrFail(gameId);
				game.StartGame();
				result = new();
			}
			catch (ServerException err)
			{
				result = new(err);
			}
			return Task.FromResult(result);
		}
	}
}
