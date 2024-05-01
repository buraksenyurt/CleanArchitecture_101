using GamersWorld.Application.Dtos.Games;

namespace GamersWorld.Application.Games.Queries.GetArchivedGames;

public class ArchivedGamesViewModel{
    public IList<GameDto> GameList { get; set; }
}