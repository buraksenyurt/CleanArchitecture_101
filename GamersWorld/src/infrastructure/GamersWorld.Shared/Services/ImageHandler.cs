using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Application.Dtos.Games;

namespace GamersWorld.Shared.Services;

public class ImageHandler
    : IImageHandler
{
    public async Task<Thumbnail> LoadWithGuidAsync(Guid guid)
    {
        var imagePath = Path.Combine("wwwroot", "assets", $"{guid}.png");
        if (!File.Exists(imagePath))
        {
            imagePath = Path.Combine("wwwroot", "assets", "DefaultThumbnail.png");
        }
        var content = await File.ReadAllBytesAsync(imagePath);
        return new Thumbnail
        {
            Id = Guid.NewGuid(),
            Content = content
        };
    }
}