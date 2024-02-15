
using AutoMapper;
using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Application.Common.Mappings;
using GamersWorld.Application.Games.Queries.GetGames;
using GamersWorld.Domain.Entities;
using GamersWorld.Domain.Enums;
using MockQueryable.Moq;
using Moq;

namespace Application.Tests;

public class GetGamesQueryTests
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private readonly IMapper _mapper;

    public GetGamesQueryTests()
    {
        var configProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        _mapper = configProvider.CreateMapper();
        _mockContext = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_ReturnsCorrectGamesCountAndData()
    {
        // Arrange
        var games = new List<Game>
        {
            new(){
                    Id=1,
                    Title="Doom IV",
                    ListPrice=34.50M,
                    Point=5.56,
                    Status=Status.OutOfSale,
                    Image=new byte[1024]
                },
                new(){
                    Id=6,
                    Title="Demolitian Man II",
                    ListPrice=24.50M,
                    Point=6.56,
                    Status=Status.OnSale,
                    Image=new byte[1024]
                }
        };

        var mockDbSet = games.AsQueryable().BuildMockDbSet();
        _mockContext.Setup(c => c.Games).Returns(mockDbSet.Object);

        var query = new GetGamesQuery();
        var handler = new GetGamesQueryHandler(_mockContext.Object, _mapper);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(games.Count, result.GameList.Count);
    }

    [Fact]
    public async Task Handle_NoGamesExist_ReturnsEmptyList()
    {
        // Arrange
        var games = new List<Game>();

        var mockDbSet = games.AsQueryable().BuildMockDbSet();
        _mockContext.Setup(c => c.Games).Returns(mockDbSet.Object);

        var query = new GetGamesQuery();
        var handler = new GetGamesQueryHandler(_mockContext.Object, _mapper);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.GameList);
    }

    [Fact]
    public async Task Handle_DbContextThrowsException_Throws()
    {
        // Arrange
        _mockContext.Setup(c => c.Games).Throws(new Exception("Database error"));

        var query = new GetGamesQuery();
        var handler = new GetGamesQueryHandler(_mockContext.Object, _mapper);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, CancellationToken.None));
    }
}
