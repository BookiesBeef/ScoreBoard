using ScoreBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.UnitTests;

public class GameBuilder
{
    private Game _game;

    private GameBuilder(Team home, Team away)
    {
        _game = new Game(home, away, DateTime.UtcNow);
    }

    public static GameBuilder Init(Team home, Team away)
    {
        return new GameBuilder(home, away);
    }

    public Game Build() => _game;
}