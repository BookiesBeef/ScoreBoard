
using ScoreBoard.Models;

namespace ScoreBoard.Interfaces;

public interface IScoreBoardService
{
    Task AddGame(string local, string away);
    Task RemoveGame(Game game);
    Task UpdateGame(Game game);
    Task<List<Game>> GetGames();
}