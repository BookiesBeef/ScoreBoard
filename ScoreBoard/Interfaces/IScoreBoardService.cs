
using ScoreBoard.Models;

namespace ScoreBoard.Interfaces;

public interface IScoreBoardService
{
    Task<string> AddGame(string local, string away);
    Task RemoveGame(string id);
    Task UpdateGame(Game game);
    Task<List<Game>> GetGames();
}