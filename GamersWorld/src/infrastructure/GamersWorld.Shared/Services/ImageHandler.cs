using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Application.Dtos.Games;

namespace GamersWorld.Shared.Services;

public class ImageHandler
    : IImageHandler
{
    public async Task<Thumbnail> LoadWithGuidAsync(Guid guid)
    {
        return new Thumbnail
        {
            Id = Guid.NewGuid(),
            Content = new byte[1024]
        };
    }
}