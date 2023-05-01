namespace ScoreBoard.Models;


public record Game(Team HomeTeam, Team AwayTeam, DateTime StartTime)
{
    public string Id => $"{HomeTeam?.Name}-{AwayTeam?.Name}".ToLower();
    public uint TotalScore => HomeTeam.Score + AwayTeam.Score;
}