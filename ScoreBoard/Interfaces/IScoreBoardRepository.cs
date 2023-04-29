
using ScoreBoard.Models;

namespace ScoreBoard.Interfaces;

public interface IScoreBoardRepository
{
    Task<IEnumerable<Game>> GetGames();
}