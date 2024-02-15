using GamersWorld.Application.Common.Exceptions;
using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Application.Games.Commands.UpdateGame;
using GamersWorld.Domain.Entities;
using GamersWorld.Domain.Enums;
using Moq;
using Moq.EntityFrameworkCore;

namespace Application.Tests;

public class UpdateGameCommandTests
{
    [Fact(Skip = "Moq Hatası nedeniyle geçici olarak atlandı")]
    public async Task UpdateGame_WhenGameExists_ShouldUpdatePropertiesAndSaveChanges()
    {
        // Arange
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
        var gameIdToUpdate = 1;
        var newTitle = "Doom VI";

        mockContext.Setup(m => m.Games).ReturnsDbSet(games);
        mockContext.Setup(m => m.Games.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync((object[] ids, CancellationToken token) => games.FirstOrDefault(g => g.Id == gameIdToUpdate));
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new UpdateGameCommandHandler(mockContext.Object, new Mock<IImageHandler>().Object);
        var command = new UpdateGameCommand
        {
            GameId = gameIdToUpdate,
            Title = newTitle
        };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedGame = games.FirstOrDefault(g => g.Id == gameIdToUpdate);
        Assert.NotNull(updatedGame);
        Assert.Equal(newTitle, updatedGame.Title);
        mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateGame_WhenGameDoesNotExist_ShouldThrowGameNotFoundException()
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
        var gameId = 999;
        mockContext.Setup(m => m.Games).ReturnsDbSet(games);
        mockContext.Setup(m => m.Games.FindAsync(999, default)).ReturnsAsync((Game)null);

        var handler = new UpdateGameCommandHandler(mockContext.Object, new Mock<IImageHandler>().Object);
        var command = new UpdateGameCommand { GameId = gameId };

        // Act
        await Assert.ThrowsAsync<GameNotFoundException>(() => handler.Handle(command, default));
    }
}