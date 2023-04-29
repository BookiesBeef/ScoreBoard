namespace ScoreBoard.Models;


public record Game(Team HomeTeam, Team AwayTeam, DateTime StartTime)
{
    public int TotalScore => HomeTeam.Score + AwayTeam.Score;
}