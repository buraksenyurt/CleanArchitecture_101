using GamersWorld.Application.Dtos.Games;

namespace GamersWorld.Application.Games.Queries.GetGamesByPaging;

public class GamesByPagingViewModel
{
    public int PageNo { get; set; }
    public int Count { get; set; }
    public IList<GameDto> GameList { get; set; }
}