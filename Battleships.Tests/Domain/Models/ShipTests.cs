using Battleships.Domain.Enums;
using Battleships.Domain.Models;
using Xunit;

namespace Battleships.Tests.Domain.Models;

public class ShipTests
{
    [Fact]
    public void Ship_HasCorrectValues()
    {
        // arrange
        const string name = "ShipName";
        const int size = 10;

        // act
        var ship = new Ship(name, size);

        // assert
        Assert.Equal(name, ship.Name);
        Assert.Equal(size, ship.Size);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void Ship_ThrowsArgumentNullException(string name)
    {
        // act & assert
        Assert.Throws<ArgumentNullException>(() => new Ship(name, 5));
    }

    [Theory]
    [InlineData(CellStatus.Hit, true)]
    [InlineData(CellStatus.Sunk, true)]
    [InlineData(CellStatus.Occupied, false)]
    public void Ship_IsSunk_ReturnsValue(CellStatus status, bool expectedResult)
    {
        // Arrange
        var ship = new Ship("TestShip", 3);
        var cells = Enumerable.Repeat(new Cell { Status = status }, 3).ToList();
        ship.OccupiedCells.AddRange(cells);

        // Act
        var result = ship.IsSunk();

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(3, CellStatus.Empty, true)]
    [InlineData(3, CellStatus.Occupied, false)]
    [InlineData(2, CellStatus.Empty, false)]
    public void Ship_TryPlaceShip_ReturnsValue(int shipSize, CellStatus cellStatus, bool expectedResult)
    {
        // Arrange
        var ship = new Ship("TestShip", shipSize);
        var cells = new List<Cell>
        {
            new() { Status = cellStatus },
            new() { Status = CellStatus.Empty },
            new() { Status = CellStatus.Empty }
        };

        // Act
        var result = ship.TryPlaceShip(cells);

        // Assert
        Assert.Equal(expectedResult, result);
        if (expectedResult)
        {
            foreach (var cell in cells)
            {
                Assert.Equal(CellStatus.Occupied, cell.Status);
                Assert.Contains(cell, ship.OccupiedCells);
            }
        }
    }
}