namespace GamersWorld.Application.Games.Queries.ExportGames;

public class ExportGamesViewModel{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Content { get; set; }
}