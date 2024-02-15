using GamersWorld.Application.Common.Interfaces;

namespace GamersWorld.Shared.Services;

public class FileWrapper 
    : IFileWrapper
{
    public async Task<byte[]> ReadAllBytesAsync(string path)
    {
        return await File.ReadAllBytesAsync(path);
    }
}