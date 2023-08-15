using Battleships.Application.Services.Implementations;
using Battleships.Application.Services.Interfaces;
using Battleships.Common.Configuration;
using Battleships.Common.Providers.Implementations;
using Battleships.Domain.Enums;
using Microsoft.Extensions.Options;
using Xunit;

namespace Battleships.Tests.Application.Services;

public class ShipPlacementServiceTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void ShipPlacementService_PlaceShipsOnBoard_PlacesShips(GameOptions gameOptions)
    {
        // Arrange
        var boardProvider = new BoardProvider(new OptionsWrapper<GameOptions>(gameOptions));
        var shipsProvider = new ShipsProvider(new OptionsWrapper<GameOptions>(gameOptions));
        IShipPlacementService shipPlacementService = new ShipPlacementService(boardProvider, shipsProvider);

        // Act
        shipPlacementService.PlaceShipsOnBoard();

        // Assert
        foreach (var cell in boardProvider.Board.Cells)
        {
            if (cell.Status == CellStatus.Occupied)
                Assert.Contains(shipsProvider.Ships, ship => ship.OccupiedCells.Contains(cell));
            else
                Assert.Equal(CellStatus.Empty, cell.Status);
        }
    }

    public static IEnumerable<object[]> TestData()
    {
        yield return new object[]
        {
            new GameOptions
            {
                BoardSize = 10,
                ShipOptions = new List<ShipOptions>
                {
                    new ShipOptions
                    {
                        Name = "Battleship",
                        Size = 5,
                        Instances = 1
                    },
                    new ShipOptions
                    {
                        Name = "Destroyer",
                        Size = 4,
                        Instances = 2
                    }
                }
            }
        };

        yield return new object[]
        {
            new GameOptions
            {
                BoardSize = 10,
                ShipOptions = new List<ShipOptions>
                {
                    new ShipOptions
                    {
                        Name = "Battleship",
                        Size = 5,
                        Instances = 2
                    },
                    new ShipOptions
                    {
                        Name = "Destroyer",
                        Size = 4,
                        Instances = 3
                    }
                }
            }
        };
    }
}