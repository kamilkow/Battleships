using Battleships.Domain.Enums;

namespace Battleships.Domain.Models;

public class Cell
{
    public CellStatus Status { get; set; } = CellStatus.Empty;
}