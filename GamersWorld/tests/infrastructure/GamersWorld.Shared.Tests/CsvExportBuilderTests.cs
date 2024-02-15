using GamersWorld.Application.Games.Queries.ExportGames;
using GamersWorld.Domain.Enums;
using GamersWorld.Shared.Services;

namespace Shared.Tests;

public class CsvExportBuilderTests
{
    [Fact]
    public void BuildFile_GivenValidGameRecords_ShouldReturnValidCsvBytes()
    {
        // Arrange
        var exportBuilder = new CsvExportBuilder();
        var games = new List<GameRecord>
        {
            new() { Title = "Zelda III", ListPrice = 59.99M,Status=Status.OnSale },
            new() { Title = "Paper Boy", ListPrice = 19.99M, Status=Status.OutOfSale}
        };

        // Act
        var result = exportBuilder.BuildFile(games);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
    }
}