namespace GamersWorld.Application.Common.Exceptions;

public class GameNotFoundException(int gameId)
        : Exception($"{gameId} not found")
{
}