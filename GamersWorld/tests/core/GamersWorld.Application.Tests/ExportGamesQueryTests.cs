
using AutoMapper;
using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Application.Common.Mappings;
using GamersWorld.Application.Games.Queries.ExportGames;
using GamersWorld.Application.Games.Queries.GetGames;
using GamersWorld.Domain.Entities;
using GamersWorld.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace Application.Tests;

public class ExportGamesQueryTests
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private readonly Mock<IExportBuilder> _mockCsvBuilder;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ExportGamesQueryHandler _handler;

    public ExportGamesQueryTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _mockMapper = new Mock<IMapper>();
        _mockCsvBuilder = new Mock<IExportBuilder>();

        _handler = new ExportGamesQueryHandler(_mockContext.Object, _mockMapper.Object, _mockCsvBuilder.Object);
    }

    [Fact]
    public async Task Handle_ReturnsExportGamesViewModel_WithCorrectProperties()
    {
        // Arrange
        var gameRecords = new List<GameRecord> {
            new() {Title="Sim City 2000", ListPrice=13.45M,Status=Status.OnSale},
            new() {Title="Emelyn Hughes Soccer", ListPrice=3.99M,Status=Status.OnSale},
            new() {Title="Tetris 3000", ListPrice=1.00M,Status=Status.OnSale},
         };

        var games = new List<Game>
        {
            new(){
                    Id=1,
                    Title="Sim City 2000",
                    ListPrice=13.45M,
                    Point=5.56,
                    Status=Status.OnSale,
                    Image=new byte[1024]
                },
                new(){
                    Id=6,
                    Title="Emelyn Hughes Soccer",
                    ListPrice=3.99M,
                    Point=6.56,
                    Status=Status.OnSale,
                    Image=new byte[1024]
                },
                new(){
                    Id=8,
                    Title="Tetris 3000",
                    ListPrice=1.00M,
                    Point=6.56,
                    Status=Status.OnSale,
                    Image=new byte[1024]
                }
        };
        var gamesQueryable = gameRecords.AsQueryable();
        var mockDbSet = games.AsQueryable().BuildMockDbSet();
        _mockContext.Setup(c => c.Games).Returns(mockDbSet.Object);
        _mockMapper.Setup(m => m.ConfigurationProvider)
            .Returns(() => new MapperConfiguration(cfg => { cfg.CreateMap<Game, GameRecord>(); }));
        _mockCsvBuilder.Setup(b => b.BuildFile(It.IsAny<IEnumerable<GameRecord>>()))
            .Returns(new byte[] { });

        // Act
        var result = await _handler.Handle(new ExportGamesQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Games.csv", result.FileName);
        Assert.Equal("text/csv", result.ContentType);
        Assert.NotNull(result.Content);
    }
}
