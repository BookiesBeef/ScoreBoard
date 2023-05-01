using AutoFixture;
using AutoFixture.AutoMoq;
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
        var fixture = new Fixture().Customize(new AutoMoqCustomization());

        var game = GameBuilder.Init(TeamBuilder.Init("Spain").Build(),
                                    TeamBuilder.Init("Sweden").Build())
                              .Build();

        var repositoryMock = fixture.Freeze<Mock<IScoreBoardRepository>>();
        repositoryMock.Setup(x => x.CreateGame(It.IsAny<Game>()));

        var sut = fixture.Create<Services.ScoreBoard>();

        //Act
        await sut.AddGame(game.HomeTeam.Name, game.AwayTeam.Name);

        //Assert
        repositoryMock.Verify(repo => repo.CreateGame(It.IsAny<Game>()), Times.Once);
    }

    [Fact]
    public async Task AddGame_ShouldReturnEmpty_WhenGameExists()
    {
        //Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var game = GameBuilder.Init(TeamBuilder.Init("Spain").Build(),
                                    TeamBuilder.Init("Sweden").Build())
                              .Build();

        var repositoryMock = fixture.Freeze<Mock<IScoreBoardRepository>>();
        repositoryMock.Setup(x => x.CreateGame(It.IsAny<Game>()));
        repositoryMock.Setup(x => x.GetGames()).ReturnsAsync(new List<Game>() { game });

        var sut = fixture.Create<Services.ScoreBoard>();

        //Act
        var result = await sut.AddGame(game.HomeTeam.Name, game.AwayTeam.Name);

        //Assert
        Assert.True(result == string.Empty);
    }

    [Fact]
    public async Task UpdateScore_ShouldUpdateBothTeamsScore()
    {
        //Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());

        var game = GameBuilder.Init(TeamBuilder.Init("Spain").Build(),
                                    TeamBuilder.Init("Sweden").Build())
                              .Build();

        var repositoryMock = fixture.Freeze<Mock<IScoreBoardRepository>>();
        repositoryMock.Setup(x => x.GetGame(It.IsAny<string>()));
        repositoryMock.Setup(x => x.GetGames());
        repositoryMock.Setup(x => x.UpdateGame(It.IsAny<Game>()));

        var sut = fixture.Create<Services.ScoreBoard>();

        _ = await sut.AddGame(game.HomeTeam.Name, game.AwayTeam.Name);

        //Act
        var updated = GameBuilder.Init(TeamBuilder.Init("Spain").WithScore(1).Build(),
                                       TeamBuilder.Init("Sweden").WithScore(3).Build())
                                 .Build();

        await sut.UpdateGame(updated);

        //Assert
        repositoryMock.Verify(service => service.UpdateGame(It.IsAny<Game>()), Times.Once);
    }

    [Fact]
    public async Task FinishGame_ShouldRemoveGame()
    {
        //Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var game = GameBuilder.Init(TeamBuilder.Init("Spain").WithScore(1).Build(),
                                    TeamBuilder.Init("Sweden").WithScore(3).Build())
                              .Build();

        var repositoryMock = fixture.Freeze<Mock<IScoreBoardRepository>>();
        repositoryMock.Setup(x => x.DeleteGame(It.IsAny<string>())).ReturnsAsync(true);

        var sut = fixture.Create<Services.ScoreBoard>();

        //Act
        await sut.RemoveGame(game.Id);
        var current = await sut.GetGames();

        //Assert
        repositoryMock.Verify(repo => repo.DeleteGame(game.Id), Times.Once);
        Assert.True(current?.Count == 0);
    }

    [Fact]
    public async Task GetGames_ShouldReturnOrderedCollection()
    {
        //Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());

        var game1 = GameBuilder.Init(TeamBuilder.Init("Spain").WithScore(1).Build(),
                                     TeamBuilder.Init("Sweden").WithScore(3).Build())
                               .Build();

        var game2 = GameBuilder.Init(TeamBuilder.Init("Portugal").WithScore(5).Build(),
                                      TeamBuilder.Init("France").WithScore(2).Build())
                                .Build();

        var game3 = GameBuilder.Init(TeamBuilder.Init("Italy").WithScore(4).Build(),
                                     TeamBuilder.Init("England").WithScore(3).Build())
                               .Build();

        var repositoryMock = fixture.Freeze<Mock<IScoreBoardRepository>>();
        repositoryMock.Setup(m => m.GetGames()).ReturnsAsync(new List<Game> { game1, game2, game3 });

        var sut = fixture.Create<Services.ScoreBoard>();

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