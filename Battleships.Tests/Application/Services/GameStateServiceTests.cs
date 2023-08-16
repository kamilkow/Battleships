using Battleships.Application.Services.Implementations;
using Battleships.Common.Providers.Interfaces;
using Battleships.Domain.Enums;
using Battleships.Domain.Models;
using NSubstitute;
using Xunit;

namespace Battleships.Tests.Application.Services;

public class GameStateServiceTests
{
    [Theory]
    [InlineData(CellStatus.Sunk, true)]
    [InlineData(CellStatus.Hit, true)]
    [InlineData(CellStatus.Occupied, false)]
    public void GameStateService_IsGameFinished_ReturnValue(CellStatus status, bool expectedResult)
    {
        // arrange
        var ships = new List<Ship>
        {
            new("ExampleName", 5),
            new("ExampleName", 5),
            new("ExampleName", 5),
        };
        ships.ForEach(s => s.OccupiedCells.AddRange(Enumerable.Repeat(new Cell { Status = status }, s.Size - 1)));
        var shipsProvider = Substitute.For<IShipsProvider>();
        shipsProvider.Ships.Returns(ships);

        var gameStateService = new GameStateService(shipsProvider);

        // act
        var result = gameStateService.IsGameFinished();

        // assert
        Assert.Equal(expectedResult, result);
    }
}