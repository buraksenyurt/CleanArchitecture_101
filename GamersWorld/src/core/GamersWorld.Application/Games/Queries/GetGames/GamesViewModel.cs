using GamersWorld.Application.Dtos.Games;

namespace GamersWorld.Application.Games.Queries.GetGames;

public class GamesViewModel{
    public IList<GameDto> GameList { get; set; }
    //public int GameCount { get; set; }
}