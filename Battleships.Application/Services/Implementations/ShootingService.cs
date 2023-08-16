using Battleships.Application.Services.Interfaces;
using Battleships.Common.Providers.Interfaces;
using Battleships.Domain.Enums;
using Battleships.Domain.Models;

namespace Battleships.Application.Services.Implementations;

public class ShootingService : IShootingService
{
    private readonly IShipsProvider _shipsProvider;
    private readonly IBoardProvider _boardProvider;

    public ShootingService(IBoardProvider boardProvider, IShipsProvider shipsProvider)
    {
        _boardProvider = boardProvider ?? throw new ArgumentNullException(nameof(boardProvider));
        _shipsProvider = shipsProvider ?? throw new ArgumentNullException(nameof(shipsProvider));
    }

    public Ship? LastHitShip { get; private set; }

    public ShotResult ShootAtCoordinates(int col, int row)
    {
        var board = _boardProvider.Board;

        var cell = board.GetCellForCoordinates(col, row);

        if (cell.Status == CellStatus.Occupied || cell.Status == CellStatus.Hit || cell.Status == CellStatus.Sunk)
        {
            cell.Status = CellStatus.Hit;

            var ship = _shipsProvider.Ships.Find(s => s.OccupiedCells.Contains(cell));
            if (ship != null)
            {
                LastHitShip = ship;

                if (ship.IsSunk())
                {
                    ship.OccupiedCells.ForEach(c => c.Status = CellStatus.Sunk);
                    return ShotResult.Sunk;
                }

                return ShotResult.Hit;
            }
        }

        cell.Status = CellStatus.Miss;
        return ShotResult.Miss;
    }
}