namespace GamersWorld.Application.Common.Interfaces;

public interface IImageHandler
{
    byte[] LoadWithGuidAsync(Guid guid);
}