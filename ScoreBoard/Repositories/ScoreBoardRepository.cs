using ScoreBoard.Interfaces;
using ScoreBoard.Models;

namespace ScoreBoard.Repositories;

public class ScoreBoardRepository : IScoreBoardRepository
{
    private readonly List<Game> _games = new List<Game>();

    public async Task CreateGame(Game game)
    {
        await Task.Run(() =>
         {
             _games.Add(game);
         });
    }

    public async Task<bool> DeleteGame(string id)
    {
        return await Task.Run(() =>
         {
             var game = _games.Find(x => x.Id == id);
             if (game != null)
             {
                 _games.Remove(game);
                 return true;
             }
             return false;
         });
    }

    public async Task<Game?> GetGame(string id)
    {
        return await Task.Run(() => _games.Find(x => x.Id == id));
    }

    public async Task<IEnumerable<Game>> GetGames()
    {
        return await Task.Run(() => _games);
    }

    public async Task<bool> UpdateGame(Game game)
    {
        return await Task.Run(() =>
          {
              _games.Remove(game);
              _games.Add(game);
              return true;
          });
    }
}
