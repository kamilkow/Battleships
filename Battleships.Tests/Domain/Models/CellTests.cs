using Battleships.Domain.Enums;
using Battleships.Domain.Models;
using Xunit;

namespace Battleships.Tests.Domain.Models;

public class CellTests
{
    [Fact]
    public void Cell_Status_IsEmpty()
    {
        // arrange
        var cell = new Cell();

        // assert
        Assert.Equal(CellStatus.Empty, cell.Status);
    }
}