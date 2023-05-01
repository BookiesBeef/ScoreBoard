
using ScoreBoard.Models;

namespace ScoreBoard.Interfaces;

public interface IScoreBoardRepository
{
    Task AddGame(Game game);
    Task<Game> GetGame(string id);
    Task<IEnumerable<Game>> GetGames();
}