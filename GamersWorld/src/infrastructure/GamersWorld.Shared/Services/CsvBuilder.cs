using System.Globalization;
using CsvHelper;
using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Application.Games.Queries.ExportGames;

namespace GamersWorld.Shared.Services;

public class CsvBuilder
    : ICsvBuilder
{
    public byte[] BuildFile(IEnumerable<GameRecord> games)
    {
        using var ms = new MemoryStream();
        using (var writer = new StreamWriter(ms))
        {
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(games);
        }
        return ms.ToArray();
    }
}