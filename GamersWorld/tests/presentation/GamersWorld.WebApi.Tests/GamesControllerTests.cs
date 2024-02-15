using GamersWorld.Application.Common.Exceptions;
using GamersWorld.Application.Dtos.Games;
using GamersWorld.Application.Games.Commands.CreateGame;
using GamersWorld.Application.Games.Commands.DeleteGame;
using GamersWorld.Application.Games.Commands.UpdateGame;
using GamersWorld.Application.Games.Queries.ExportGames;
using GamersWorld.Application.Games.Queries.GetGames;
using GamersWorld.Domain.Enums;
using GamersWorld.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApi.Tests;

public class GamesControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly GamesController _controller;

    public GamesControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new GamesController(_mockMediator.Object);
    }

    [Fact]
    public async Task Get_ShouldReturnGamesViewModel()
    {
        // Arrange
        var mockGames = new GamesViewModel
        {
            GameList =
            [
                new GameDto{
                    Id=1,
                    Title="Lotus III",
                    Status=(short)Status.OutOfSale,
                    ListPrice=3.67M,
                    Point=4.9
                },
                new GameDto{
                    Id=2,
                    Title="Red Alert II",
                    Status=(short)Status.OnSale,
                    ListPrice=55.67M,
                    Point=8.9
                },
                new GameDto{
                    Id=3,
                    Title="Warcraft II",
                    Status=(short)Status.OnSale,
                    ListPrice=40.00M,
                    Point=9.1
                }
            ]
        };
        _mockMediator.Setup(m => m.Send(It.IsAny<GetGamesQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(mockGames);

        // Act
        var result = await _controller.Get();

        // Assert
        var actionResult = Assert.IsType<ActionResult<GamesViewModel>>(result);
        var model = Assert.IsAssignableFrom<GamesViewModel>(actionResult.Value);
    }

    [Fact]
    public async Task Create_ShouldReturnGameId_OnSuccess()
    {
        // Arrange
        _mockMediator.Setup(m => m.Send(It.IsAny<CreateGameCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(1);

        // Act
        var result = await _controller.Create(new CreateGameCommand());

        // Assert
        var actionResult = Assert.IsType<ActionResult<int>>(result);
        Assert.Equal(1, actionResult.Value);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_OnSuccess()
    {
        // Arrange
        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteGameCommand>(), It.IsAny<CancellationToken>()))
                     .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenGameNotFoundException()
    {
        // Arrange
        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteGameCommand>(), It.IsAny<CancellationToken>()))
                     .Throws(new GameNotFoundException(1));

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Export_ShouldReturnExportGamesViewModel()
    {
        // Arrange
        var mockExport = new ExportGamesViewModel
        {
            Content = new byte[1024],
            ContentType = "text/csv",
            FileName = "Games.csv"
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<ExportGamesQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(mockExport);

        // Act
        var result = await _controller.Export();

        // Assert
        var actionResult = Assert.IsType<ActionResult<ExportGamesViewModel>>(result);
        var model = Assert.IsAssignableFrom<ExportGamesViewModel>(actionResult.Value);
    }
}