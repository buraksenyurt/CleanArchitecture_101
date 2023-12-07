using GamersWorld.Application.Common.Mappings;
using GamersWorld.Domain.Entities;
using GamersWorld.Domain.Enums;

namespace GamersWorld.Application.Games.Queries.ExportGames;

public class GameRecord
    : IMapFrom<Game>
{
    public string Title { get; set; }
    public decimal ListPrice { get; set; }
    public Status Status { get; set; }
}