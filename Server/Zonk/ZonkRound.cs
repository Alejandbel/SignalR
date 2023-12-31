﻿using Domain;
using Domain.Shared;
using Server.Excpetions;

namespace Server.Zonk
{
	public class ZonkRound
	{
		static private int scoreToEnd = 300;

		private Dictionary<string, PlayerMove> playersMove = new();
		public Dictionary<string, PlayerMove> PlayersMove { get => playersMove; }

		public bool IsEnded { get => playersMove.Values.All(p => p.IsEnded); }

		public ZonkRound(IEnumerable<string> players)
		{
			foreach (var player in players)
			{
				playersMove.Add(player, new() { Name = player });
			}
		}

		public int GetScore(string name)
		{
			var player = GetPlayerOrFail(name);
			return player.Score;
		}

		public void EndMove(string name, IEnumerable<int> dices)
		{
			var player = GetNotEndedPlayerOrFail(name);

			AssertDicesValid(dices, player);

			var score = ZonkUtils.GetScore(dices);
			var newScore = player.Score + score;

			if (newScore < scoreToEnd)
			{
				throw new ServerException($"Score must be above {scoreToEnd} to end move");
			}

			player.Score = newScore;
			player.IsEnded = true;
		}

		public IEnumerable<int> RollDices(string name)
		{
			var player = GetNotEndedPlayerOrFail(name);

			if (player.Dices != null)
			{
				throw new ServerException("Dices already roled");
			}

			var dices = ZonkUtils.RollDices(6);
			player.Dices = dices;

			if (!ZonkUtils.IsAbleToMove(dices))
			{
                player.IsZonked = true;
                player.IsEnded = true;
                player.Score = 0;
                player.Dices = null;
                player.DiceCount = 0;
            }

			return dices;
		}

		public IEnumerable<int> RerollDices(string name, IEnumerable<int> dices)
		{
			var player = GetPlayerOrFail(name);
			AssertDicesValid(dices, player);
			player.DiceCount -= dices.Count();

			var score = ZonkUtils.GetScore(dices);
			player.Score += score;

			if (player.DiceCount == 0)
			{
				//Bonus move
				player.DiceCount = 6;
			}
			var newDices = ZonkUtils.RollDices(player.DiceCount);

			if (!ZonkUtils.IsAbleToMove(newDices))
			{
				player.IsZonked = true;
				player.IsEnded = true;
				player.Score = 0;
				player.Dices = null;
				player.DiceCount = 0;
				return newDices;
			}

			player.Dices = newDices;

			return newDices;
		}

		private static void AssertDicesValid(IEnumerable<int> dices, PlayerMove player)
		{
			if (player.Dices == null)
			{
				throw new ServerException("To update score you have to role dices first");
			}

			if (!ZonkUtils.MoveCanBeMade(dices))
			{
				throw new ServerException("Move cannot be made, check your dices");
			} 

			foreach (var dice in dices)
			{
				if (!player.Dices.Contains(dice))
				{
					throw new ServerException($"Invalid dice provided: {dice}");
				}
			}
		}

		private PlayerMove GetPlayerOrFail(string name)
		{
			PlayerMove player;
			if (!playersMove.TryGetValue(name, out player))
			{
				throw new ServerException("User not found in game");
			}

			return player;
		}

		private PlayerMove GetNotEndedPlayerOrFail(string name)
		{
			var player = GetPlayerOrFail(name);

			if (player.IsEnded)
			{
				throw new ServerException($"Move already ended");
			}

			return player;
		}
	}
}
