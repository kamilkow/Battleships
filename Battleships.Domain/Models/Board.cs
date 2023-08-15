using Battleships.Domain.Enums;

namespace Battleships.Domain.Models;

public class Board
{
    public Board(int size)
    {
        if (size is <= 0 or > 26)
            throw new ArgumentOutOfRangeException(nameof(size));

        Size = size;
        Cells = InitializeCells(size);
    }

    public int Size { get; }

    public Cell[,] Cells { get; }

    public virtual Cell GetCellForCoordinates(int col, int row)
    {
        if (!IsValidPosition(col, row))
        {
            return new Cell { Status = CellStatus.Invalid };
        }

        return Cells[row, col];
    }

    public bool IsValidPosition(int col, int row)
    {
        return col >= 0 && col < Size && row >= 0 && row < Size;
    }

    private static Cell[,] InitializeCells(int size)
    {
        var cells = new Cell[size, size];

        for (var row = 0; row < size; row++)
        {
            for (var col = 0; col < size; col++)
            {
                cells[row, col] = new Cell();
            }
        }

        return cells;
    }
}