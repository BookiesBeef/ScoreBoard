
using ScoreBoard.Models;

namespace ScoreBoard.Interfaces;

public interface IScoreBoardRepository
{
    Task CreateGame(Game game);    
    Task<bool> UpdateGame(Game game);
    Task<bool> DeleteGame(string id);
    Task<Game?> GetGame(string id);
    Task<IEnumerable<Game>> GetGames();
}