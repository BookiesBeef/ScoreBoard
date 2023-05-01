using ScoreBoard.Interfaces;
using ScoreBoard.Models;

namespace ScoreBoard.Services;

public class ScoreBoard : IScoreBoardService
{
    private readonly IScoreBoardRepository _repository;

    public ScoreBoard(IScoreBoardRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> AddGame(string local, string away)
    {
        if (string.IsNullOrEmpty(local)) throw new ArgumentNullException(local);
        if (string.IsNullOrEmpty(away)) throw new ArgumentNullException(away);

        var game = new Game(new Team(local), new Team(away), DateTime.UtcNow);

        var games = await _repository.GetGames();

        if (games.Select(g => g.Id).Contains(game.Id))
            return string.Empty;

        await _repository.CreateGame(game);
        return game.Id;
    }

    public async Task<List<Game>> GetGames()
    {
        var games = await _repository.GetGames();

        return games.OrderByDescending(g => g.TotalScore).ThenByDescending(g => g.StartTime).ToList();
    }

    public async Task RemoveGame(string id)
    {
        if(string.IsNullOrEmpty(id)) throw new ArgumentNullException(id);

        if (!await _repository.DeleteGame(id))
            throw new ArgumentException("Game does not exist.");
        
    }

    public async Task UpdateGame(Game game)
    {
        await _repository.UpdateGame(game);
    }
}
