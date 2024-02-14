using GamersWorld.Application.Dtos.Games;

namespace GamersWorld.Application.Common.Interfaces;

public interface IImageHandler
{
    Task<Thumbnail> LoadWithGuidAsync(Guid guid);
}