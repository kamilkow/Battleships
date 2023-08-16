using Battleships.Application.Services.Interfaces;
using Battleships.Common.Providers.Interfaces;
using Battleships.Domain.Enums;
using Battleships.Domain.Models;

namespace Battleships.Application.Services.Implementations;

public class ShipPlacementService : IShipPlacementService
{
    private readonly IBoardProvider _boardProvider;
    private readonly IShipsProvider _shipsProvider;
    private readonly Random _random;

    public ShipPlacementService(IBoardProvider boardProvider, IShipsProvider shipsProvider)
    {
        _boardProvider = boardProvider ?? throw new ArgumentNullException(nameof(boardProvider));
        _shipsProvider = shipsProvider ?? throw new ArgumentNullException(nameof(shipsProvider));
        _random = new Random();
    }

    public void PlaceShipsOnBoard()
    {
        var board = _boardProvider.Board;

        foreach (var ship in _shipsProvider.Ships)
        {
            PlaceShip(board, ship);
        }
    }

    private void PlaceShip(Board board, Ship ship)
    {
        var placed = false;

        while (!placed)
        {
            var startRow = GetRandomStartCoordinate(board.Size, ship.Size);
            var startCol = GetRandomStartCoordinate(board.Size, ship.Size);
            var orientation = GetRandomOrientation();
            var occupiedCells = new List<Cell>();

            for (var i = 0; i < ship.Size; i++)
            {
                var row = startRow + (i * (orientation == Orientation.Vertical ? 1 : 0));
                var col = startCol + (i * (orientation == Orientation.Horizontal ? 1 : 0));

                if (!board.IsValidPosition(col, row))
                {
                    break;
                }

                var cell = board.GetCellForCoordinates(col, row);
                if (cell.Status == CellStatus.Occupied)
                {
                    break;
                }

                occupiedCells.Add(cell);
            }

            if (occupiedCells.Count == ship.Size)
            {
                placed = ship.TryPlaceShip(occupiedCells);
            }
        }
    }

    private int GetRandomStartCoordinate(int boardSize, int shipSize)
    {
        return _random.Next(0, boardSize - shipSize + 1);
    }

    private Orientation GetRandomOrientation()
    {
        return (Orientation)_random.Next(2);
    }
}