﻿using Domain.Shared;

namespace Domain.Shared
{
	public interface IZonkClient
	{
		Task<ResponseDTO<IEnumerable<GameInfo>>> GetGames();
		Task<ResponseDTO<GameInfo>> GetGame(string gameId);
		Task<ResponseDTO<GameInfo>> InitializeGame(string name);
		Task<ResponseDTO> AddPlayer(string gameId, string name);
		Task<ResponseDTO<GameInfo>> StartGame(string gameId);
		Task<ResponseDTO<IEnumerable<int>>> RollDices(string gameId, string name);
		Task<ResponseDTO> EndMove(string gameId, string name, IEnumerable<int> dices);
		Task<ResponseDTO<IEnumerable<int>>> RerollDices(string gameId, string name, IEnumerable<int> dices);
	}
}
