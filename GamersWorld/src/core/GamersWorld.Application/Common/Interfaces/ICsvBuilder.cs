using GamersWorld.Application.Games.Queries.ExportGames;

namespace GamersWorld.Application.Interfaces;

public interface ICsvBuilder
{
    byte[] BuildFile(IEnumerable<GameRecord> games);
}