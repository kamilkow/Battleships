using Battleships.Common.Configuration;
using Battleships.Common.Providers.Interfaces;
using Battleships.Domain.Models;
using Microsoft.Extensions.Options;

namespace Battleships.Common.Providers.Implementations;

public class ShipsProvider : IShipsProvider
{
    public ShipsProvider(IOptions<GameOptions> gameOptions)
    {
        if (gameOptions.Value == null)
            throw new ArgumentNullException(nameof(gameOptions));

        Ships.AddRange(gameOptions.Value.ShipOptions.SelectMany(s => Enumerable.Repeat(s, s.Instances))
            .Select(s => new Ship(s.Name, s.Size)));
    }

    public List<Ship> Ships { get; } = new();
}