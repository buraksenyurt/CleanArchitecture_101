namespace GamersWorld.Application.Common.Interfaces;

public interface IArchiveService
{
    Task<int> MoveAsync(int keyId);
}