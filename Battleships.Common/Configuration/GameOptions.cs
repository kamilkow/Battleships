namespace Battleships.Common.Configuration;

public class GameOptions
{
    public int BoardSize { get; init; }

    public List<ShipOptions> ShipOptions { get; init; } = new();
}