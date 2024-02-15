namespace GamersWorld.Application.Common.Interfaces;

public interface IFileWrapper
{
    Task<byte[]> ReadAllBytesAsync(string path);
}