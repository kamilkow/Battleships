using Battleships.Domain.Enums;

namespace Battleships.Domain.Models;

public class Ship
{
    public Ship(string? name, int size)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        if (size <= 0)
            throw new ArgumentOutOfRangeException(nameof(size));

        Name = name;
        Size = size;
    }

    public string? Name { get; }

    public int Size { get; }

    public List<Cell> OccupiedCells { get; } = new();

    public bool IsSunk() => OccupiedCells.TrueForAll(c => c.Status is CellStatus.Hit or CellStatus.Sunk);

    public bool TryPlaceShip(List<Cell> cells)
    {
        if (cells.Count != Size)
            return false;

        foreach (var cell in cells)
        {
            if (cell.Status == CellStatus.Occupied)
                return false;

            cell.Status = CellStatus.Occupied;
            OccupiedCells.Add(cell);
        }

        return true;
    }
}