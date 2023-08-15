namespace Battleships.Domain.Enums;

public enum CellStatus
{
    Invalid = 0,
    Empty = 1,
    Occupied = 2,
    Miss = 3,
    Hit = 4,
    Sunk = 5,
}