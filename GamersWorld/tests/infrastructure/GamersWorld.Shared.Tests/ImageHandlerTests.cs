using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Shared.Services;
using Moq;

namespace Shared.Tests;

public class ImageHandlerTests
{
    [Fact]
    public async Task LoadWithGuidAsync_ReturnsDefaultThumbnail_WhenImageDoesNotExist()
    {
        // Arrange
        var mockFileWrapper = new Mock<IFileWrapper>();
        mockFileWrapper.Setup(f => f.ReadAllBytesAsync(It.IsAny<string>()))
                       .ReturnsAsync(new byte[1024]);

        var imageHandler = new ImageHandler(mockFileWrapper.Object);
        var guid = Guid.NewGuid();

        // Act
        var result = await imageHandler.LoadWithGuidAsync(guid);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1024, result.Content.Length);
    }
}