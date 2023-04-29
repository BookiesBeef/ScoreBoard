using Moq;
using ScoreBoard.Interfaces;

namespace ScoreBoard.UnitTests;

public class ScoreBoardService
{

    private readonly Mock<IScoreBoardService> _scoreBoard;

    public ScoreBoardService(Mock<IScoreBoardService> scoreBoard)
    {
        _scoreBoard = scoreBoard;
    }

    [Fact]
    public void AddGameToScoreBoard_WhenGameNotExists_ReturnOk()
    {

    }

    [Fact]
    public void AddGameToScoreBoard_WhenGameExists_ReturnKo()
    {

    }

    [Fact]
    public void UpdateScoreGameInScoreBoard_WhenGameExists_ReturnOk()
    {

    }

    [Fact]
    public void UpdateScoreGameInScoreBoard_WhenGameNotExists_ReturnKo()
    {

    }

    [Fact]
    public void FinishGameInScoreBoard_RemovesGame()
    {

    }
}