using Battleships.Application.Services.Implementations;
using Battleships.Application.Services.Interfaces;
using Battleships.Common.Providers.Interfaces;
using Battleships.Domain.Enums;
using Battleships.Domain.Models;
using NSubstitute;
using Xunit;

namespace Battleships.Tests.Application.Services;

public class ShootingServiceTests
{
    private readonly IShootingService _shootingService;
    private readonly IBoardProvider _boardProvider;
    private readonly IShipsProvider _shipsProvider;

    public ShootingServiceTests()
    {
        _boardProvider = Substitute.For<IBoardProvider>();
        _boardProvider.Board.Returns(new Board(10));
        _shipsProvider = Substitute.For<IShipsProvider>();
        _shootingService = new ShootingService(_boardProvider, _shipsProvider);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void ShootingService_ShootAtCoordinates_ValidStatus_ReturnsExpectedResult(CellStatus cellStatus, List<Ship> ships,
        ShotResult expectedResult)
    {
        // Arrange
        var cell = _boardProvider.Board.GetCellForCoordinates(0, 0);
        cell.Status = cellStatus;
        SetUpOccupiedCells(ships.Single(), cell);
        _shipsProvider.Ships.Returns(ships);

        // Act
        var result = _shootingService.ShootAtCoordinates(0, 0);

        // Assert
        Assert.Equal(expectedResult, result);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(CellStatus.Invalid, ShotResult.Miss)]
    [InlineData(CellStatus.Empty, ShotResult.Miss)]
    [InlineData(CellStatus.Miss, ShotResult.Miss)]
    public void ShootingService_ShootAtCoordinates_InvalidStatus_ReturnsExpectedResult(CellStatus cellStatus,
        ShotResult expectedResult)
    {
        // Arrange
        var cell = _boardProvider.Board.GetCellForCoordinates(0, 0);
        cell.Status = cellStatus;

        // Act
        var result = _shootingService.ShootAtCoordinates(0, 0);

        // Assert
        Assert.Equal(expectedResult, result);
        Assert.Equal(expectedResult, result);
    }

    public static IEnumerable<object[]> TestData()
    {
        yield return new object[]
        {
            CellStatus.Occupied,
            new List<Ship>
            {
                new("exampleName", 2)
            },
            ShotResult.Hit
        };

        yield return new object[]
        {
            CellStatus.Hit,
            new List<Ship>
            {
                new("exampleName", 2)
            },
            ShotResult.Hit
        };
    }

    private static void SetUpOccupiedCells(Ship ship, Cell cell)
    {
        var occupiedCells = Enumerable.Repeat(new Cell { Status = CellStatus.Occupied }, ship.Size - 1).ToList();
        occupiedCells.Add(cell);
        ship.OccupiedCells.AddRange(occupiedCells);
    }
}