using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Application.Dtos.Games;
using GamersWorld.Application.Games.Commands.CreateGame;
using GamersWorld.Domain.Entities;
using Moq;

namespace Application.Tests;

public class CreateGameCommandTests
{
    [Fact]
    public async Task Handle_ValidRequest_ShouldCreateGameAndReturnGameId()
    {
        // Arrange
        var expectedGameId = 123;
        var mockContext = new Mock<IApplicationDbContext>();
        var mockImageHandler = new Mock<IImageHandler>();
        var command = new CreateGameCommand
        {
            Title = "Test Game",
            Status = 1,
            Point = 9.99,
            ListPrice = 49.99M,
            ImageId = Guid.NewGuid()
        };
        var handler = new CreateGameCommandHandler(mockContext.Object, mockImageHandler.Object);

        mockImageHandler.Setup(i => i.LoadWithGuidAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Thumbnail { Id = command.ImageId, Content = new byte[1024] });

        mockContext.Setup(c => c.Games.Add(It.IsAny<Game>()))
            .Callback<Game>(g => g.Id = expectedGameId);

        mockContext.Setup(c => c.SaveChangesAsync(default))
            .ReturnsAsync(1);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.Equal(expectedGameId, result);

    }
}