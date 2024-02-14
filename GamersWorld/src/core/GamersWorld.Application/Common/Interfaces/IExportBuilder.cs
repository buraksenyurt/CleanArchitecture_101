using GamersWorld.Application.Games.Queries.ExportGames;

namespace GamersWorld.Application.Common.Interfaces;

public interface IExportBuilder
{
    byte[] BuildFile(IEnumerable<GameRecord> games);
}