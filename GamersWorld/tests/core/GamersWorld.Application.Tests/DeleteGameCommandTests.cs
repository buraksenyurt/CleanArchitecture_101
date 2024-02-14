using GamersWorld.Application.Common.Exceptions;
using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Application.Games.Commands.DeleteGame;
using GamersWorld.Domain.Entities;
using GamersWorld.Domain.Enums;
using Moq;
using Moq.EntityFrameworkCore;

namespace Application.Tests;

public class DeleteGameCommandTests
{

    [Fact]
    public async Task Handle_ValidRequest_ShouldDeleteGameWithId()
    {
        // Arrange
        var mockContext = new Mock<IApplicationDbContext>();
        IList<Game> games = [
            new(){
                Id=1,
                Title="Doom IV",
                ListPrice=34.50M,
                Point=5.56,
                Status=Status.OutOfSale,
                Image=new byte[1024]
            },
            new(){
                Id=6,
                Title="Demolitian Man II",
                ListPrice=24.50M,
                Point=6.56,
                Status=Status.OnSale,
                Image=new byte[1024]
            }
        ];
        mockContext.Setup(m => m.Games).ReturnsDbSet(games);
        var command = new DeleteGameCommand
        {
            GameId = 6
        };

        var handler = new DeleteGameCommandHandler(mockContext.Object);

        // Act
        await handler.Handle(command, default);

        mockContext.Verify(m => m.Games.Remove(games[1]), Times.Once);
        mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_GameNotFound_ThrowsGameNotFoundException()
    {
        // Arrange
        var mockContext = new Mock<IApplicationDbContext>();
        IList<Game> games = [
            new(){
                Id=1,
                Title="Doom IV",
                ListPrice=34.50M,
                Point=5.56,
                Status=Status.OutOfSale,
                Image=new byte[1024]
            },
            new(){
                Id=6,
                Title="Demolitian Man II",
                ListPrice=24.50M,
                Point=6.56,
                Status=Status.OnSale,
                Image=new byte[1024]
            }
        ];
        mockContext.Setup(m => m.Games).ReturnsDbSet(games);
        var command = new DeleteGameCommand
        {
            GameId = 999
        };
        mockContext.Setup(m => m.Games.FindAsync(It.IsAny<int>())).ReturnsAsync((Game)null);
        var handler = new DeleteGameCommandHandler(mockContext.Object);
        await Assert.ThrowsAsync<GameNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}