using FluentValidation.TestHelper;
using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Application.Dtos.Games;
using GamersWorld.Application.Games.Commands.CreateGame;
using GamersWorld.Domain.Entities;
using GamersWorld.Domain.Enums;
using Moq;

namespace Application.Tests;

public class CreateGameCommandTests
{
    private readonly CreateGameCommandValidator _validator;
    public CreateGameCommandTests()
    {
        _validator = new CreateGameCommandValidator(null);
    }

    [Fact]
    public void Should_Require_Title()
    {
        var command = new CreateGameCommand
        {
            Title = string.Empty,
            Point = 5.0,
            ListPrice = 34.50M,
            ImageId = Guid.NewGuid(),
            Status = (short)Status.PreSale
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Title)
              .WithErrorMessage("Title info required");
    }

    [Fact]
    public void Should_Fail_When_Title_Is_Too_Long()
    {
        var command = new CreateGameCommand
        {
            Title = "There is a really huge game name out of range. Please refactor it!",
            Point = 5.0,
            ListPrice = 34.50M,
            ImageId = Guid.NewGuid(),
            Status = (short)Status.PreSale
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Title)
              .WithErrorMessage("Invalid title. Too long!");
    }

    [Theory]
    [InlineData(-1.0)]
    [InlineData(10.1)]
    public void Should_Fail_When_Point_Is_Out_Of_Range(double point)
    {
        var command = new CreateGameCommand
        {
            Title = "Commandos II",
            Point = point,
            ListPrice = 34.50M,
            ImageId = Guid.NewGuid(),
            Status = (short)Status.PreSale
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Point)
              .WithErrorMessage("Invalid range!");
    }

    [Fact]
    public void Should_Pass_With_Valid_Command()
    {
        var command = new CreateGameCommand
        {
            Title = "Commandos",
            Point = 5.0,
            ListPrice = 34.50M,
            ImageId = Guid.NewGuid(),
            Status = (short)Status.PreSale
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldCreateGameAndReturnGameId()
    {
        // Arrange
        var expectedGameId = 123;
        var mockContext = new Mock<IApplicationDbContext>();
        var mockImageHandler = new Mock<IImageHandler>();
        var command = new CreateGameCommand
        {
            Title = "Doom IV",
            Status = (short)Status.OnSale,
            Point = 9.99,
            ListPrice = 49.99M,
            ImageId = Guid.NewGuid()
        };
        var thumbnail = new Thumbnail { Id = command.ImageId, Content = new byte[1024] };
        var createdGame = new Game
        {
            Id = expectedGameId,
            Title = command.Title,
            Status = Status.OnSale,
            Point = command.Point,
            ListPrice = command.ListPrice,
            Image = thumbnail.Content
        };
        var handler = new CreateGameCommandHandler(mockContext.Object, mockImageHandler.Object);

        mockImageHandler.Setup(i => i.LoadWithGuidAsync(It.IsAny<Guid>()))
            .ReturnsAsync(thumbnail);

        // mockContext.Setup(c => c.AddGameAsync(It.IsAny<Game>()))
        //     .Callback<Game>(g => g.Id = expectedGameId);

        mockContext.Setup(c => c.AddGameAsync(It.IsAny<Game>())).ReturnsAsync(createdGame);

        mockContext.Setup(c => c.SaveChangesAsync(default))
            .ReturnsAsync(1);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.Equal(1, result);
        Assert.Equal(expectedGameId, createdGame.Id);
    }
}