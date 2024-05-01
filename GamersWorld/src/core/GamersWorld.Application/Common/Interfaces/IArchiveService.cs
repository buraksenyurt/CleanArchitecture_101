namespace GamersWorld.Application.Common.Interfaces;

public interface IArchiveService
{
    Task MoveAsync(int keyId);
}