using AutoFixture;
using Moq;
using ScoreBoard.Interfaces;
using ScoreBoard.Models;

namespace ScoreBoard.UnitTests;

public class ScoreBoardServiceTests
{

    public ScoreBoardServiceTests()
    {

    }

    [Fact]
    public async Task AddGame_ShouldPersist_WhenGameNotExists()
    {
        //Arrange
        var fixture = new Fixture();
        var game = fixture.Create<Game>();
        var scoreBoardMock = fixture.Freeze<Mock<IScoreBoardService>>();
        scoreBoardMock.Setup(x => x.AddGame(It.IsAny<string>(), It.IsAny<string>()));

        var repositoryMock = new Mock<IScoreBoardRepository>();
        repositoryMock.Setup(x => x.AddGame(It.IsAny<Game>()));
        repositoryMock.Setup(x => x.GetGame(It.IsAny<string>()));

        //Act
        await scoreBoardMock.Object.AddGame(game.HomeTeam.Name, game.AwayTeam.Name);

        //Assert
        repositoryMock.Verify(sb => sb.AddGame(game), Times.Exactly(2));
    }



    [Fact]
    public async Task AddGame_ShouldThrow_WhenGameExists()
    {
        //Arrange
        var fixture = new Fixture();
        var game = fixture.Create<Game>();
        var scoreBoardMock = fixture.Freeze<Mock<IScoreBoardService>>();
        scoreBoardMock.Setup(x => x.AddGame(It.IsAny<string>(), It.IsAny<string>()));

        var repositoryMock = new Mock<IScoreBoardRepository>();
        repositoryMock.Setup(x => x.AddGame(It.IsAny<Game>()));
        repositoryMock.Setup(x => x.GetGame(It.IsAny<string>())).ReturnsAsync(game);

        //Act
        await scoreBoardMock.Object.AddGame(game.HomeTeam.Name, game.AwayTeam.Name);

        //Assert
        await Assert.ThrowsAsync<ArgumentException>(() => scoreBoardMock.Object.AddGame(game.HomeTeam.Name, game.AwayTeam.Name));
    }

    [Fact]
    public async Task UpdateScore_ShouldUpdateBothTeamsScore()
    {
        //Arrange
        var fixture = new Fixture();
        var game = fixture.Create<Game>();
        var scoreBoardMock = fixture.Freeze<Mock<IScoreBoardService>>();
        scoreBoardMock.Setup(x => x.AddGame(It.IsAny<string>(), It.IsAny<string>()));

        //Act
        await scoreBoardMock.Object.AddGame(game.HomeTeam.Name, game.AwayTeam.Name);
        await scoreBoardMock.Object.UpdateGame(game);

        //Assert
        scoreBoardMock.Verify(service => service.AddGame(game.HomeTeam.Name, game.AwayTeam.Name), Times.Once);
        scoreBoardMock.Verify(service => service.UpdateGame(game), Times.Once);
    }


    [Fact]
    public async Task FinishGame_ShouldRemoveGame()
    {
        //Arrange
        var fixture = new Fixture();
        var game = fixture.Create<Game>();
        var scoreBoardMock = fixture.Freeze<Mock<IScoreBoardService>>();
        var sut = scoreBoardMock.Object;

        //Act
        await sut.RemoveGame(game);
        var current = await sut.GetGames();

        //Assert
        scoreBoardMock.Verify(service => service.RemoveGame(game), Times.Once);
        Assert.True(current != null);
        Assert.True(current.Count == 0);
    }

    [Fact]
    public async Task GetGames_ShouldReturnOrderedCollection()
    {
        //Arrange
        var fixture = new Fixture();
        var game = fixture.Create<Game>();
        var scoreBoardMock = fixture.Freeze<Mock<IScoreBoardService>>();
        scoreBoardMock.Setup(mock => mock.GetGames());


        var game1 = GameBuilder.Init(TeamBuilder.Init("Spain").WithScore(1).Build(),
                                     TeamBuilder.Init("Sweden").WithScore(3).Build())
                               .Build();

        var game2 = GameBuilder.Init(TeamBuilder.Init("Portugal").WithScore(5).Build(),
                                      TeamBuilder.Init("France").WithScore(2).Build())
                                .Build();

        var game3 = GameBuilder.Init(TeamBuilder.Init("Italy").WithScore(4).Build(),
                                     TeamBuilder.Init("England").WithScore(3).Build())
                               .Build();

        var games = new List<Game>
       {
           game1,
           game2,
           game3
       };

        var repositoryMock = new Mock<IScoreBoardRepository>();
        repositoryMock.Setup(m => m.GetGames()).ReturnsAsync(games);

        var sut = scoreBoardMock.Object;

        //Act
        var orderedGames = await sut.GetGames();

        //Assert
        Assert.True(orderedGames != null);
        Assert.Equal(3, orderedGames.Count);
        Assert.Equal(game3, orderedGames[0]);
        Assert.Equal(game2, orderedGames[1]);
        Assert.Equal(game1, orderedGames[2]);
    }

}