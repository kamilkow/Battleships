using Battleships.Domain.Enums;
using Battleships.Domain.Models;

namespace Battleships.ConsoleUI.Extensions;

public static class CellExtensions
{
    public static char ToSymbol(this Cell cell) => cell.Status switch
    {
        CellStatus.Miss => 'M',
        CellStatus.Hit => 'X',
        CellStatus.Sunk => 'S',
        _ => '.'
    };
}