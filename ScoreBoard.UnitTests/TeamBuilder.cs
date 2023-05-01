using ScoreBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ScoreBoard.UnitTests;

public class TeamBuilder
{
    private Team _team;

    private TeamBuilder(string name)
    {
        _team = new Team(name);
    }

    public static TeamBuilder Init(string name)
    {
        return new TeamBuilder(name);
    }

    public TeamBuilder WithScore(uint score)
    {
        _team.Score = score; return this;
    }

    public Team Build() => _team;
}
