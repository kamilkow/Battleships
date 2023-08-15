using Battleships.Application.Services.Interfaces;
using Battleships.Common.Providers.Interfaces;

namespace Battleships.Application.Services.Implementations;

public class GameStateService : IGameStateService
{
    private readonly IShipsProvider _shipsProvider;

    public GameStateService(IShipsProvider shipsProvider)
    {
        _shipsProvider = shipsProvider ?? throw new ArgumentNullException(nameof(shipsProvider));
    }

    public bool IsGameFinished() => _shipsProvider.Ships.TrueForAll(s => s.IsSunk());
}