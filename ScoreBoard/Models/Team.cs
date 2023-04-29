
namespace ScoreBoard.Models;

public record Team(string Name)
{
    public uint Score { get; set; } = 0;
}