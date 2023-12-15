using Domain.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class MoqZonkClient : IZonkClient
{
    public Task<ResponseDTO> AddPlayer(string gameId, string name)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> EndMove(string gameId, string name, IEnumerable<int> dices)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO<GameInfo>> GetGame(string gameId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO<IEnumerable<GameInfo>>> GetGames()
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDTO<GameInfo>> InitializeGame(string name)
    {
        var a = new GameInfo("1", 
            new Dictionary<string, List<int>>(new List<KeyValuePair<string, List<int>>>() { new("misha", new() { 1, 2, 3, 4, 5, 6, 8, 9, 10 }), new("lol", new() { 1, 2, 3, 4, 5, 6, 8, 9, 10 }) }),
            0, null, true);
        var Response = new ResponseDTO<GameInfo>() { Data = a, Error = null, IsSuccess = true };
        return Response;
    }

    public Task<ResponseDTO<IEnumerable<int>>> RerollDices(string gameId, string name, IEnumerable<int> dices)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO<IEnumerable<int>>> RollDices(string gameId, string name)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> StartGame(string gameId)
    {
        throw new NotImplementedException();
    }
}