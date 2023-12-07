using GamersWorld.Domain.Enums;

namespace GamersWorld.Domain.Entities;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; } = "Anoynmous";
    public double Point { get; set; }
    public decimal ListPrice { get; set; }
    public Status Status { get; set; }
    public byte[] Image { get; set; }
}