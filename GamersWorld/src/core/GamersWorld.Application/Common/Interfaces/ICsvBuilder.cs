using GamersWorld.Application.Games.Queries.ExportGames;

namespace GamersWorld.Application.Common.Interfaces;

public interface ICsvBuilder
{
    byte[] BuildFile(IEnumerable<GameRecord> games);
}