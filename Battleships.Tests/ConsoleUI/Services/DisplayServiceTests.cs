using Battleships.Common.Providers.Interfaces;
using Battleships.ConsoleUI.Services.Implementations;
using Battleships.ConsoleUI.Services.Interfaces;
using Battleships.Domain.Enums;
using Battleships.Domain.Models;
using NSubstitute;
using Xunit;

namespace Battleships.Tests.ConsoleUI.Services;

public class DisplayServiceTests
{
    private readonly IBoardProvider _boardProvider = Substitute.For<IBoardProvider>();

    public DisplayServiceTests()
    {
        var board = new Board(5);
        _boardProvider.Board.Returns(board);
    }

    [Fact]
    public void DisplayService_DisplayBoard_PrintsEmptyBoard()
    {
        // Arrange
        IDisplayService displayService = new DisplayService(_boardProvider);
        var expectedOutput =
            "   A B C D E " + Environment.NewLine +
            " 1 . . . . . " + Environment.NewLine +
            " 2 . . . . . " + Environment.NewLine +
            " 3 . . . . . " + Environment.NewLine +
            " 4 . . . . . " + Environment.NewLine +
            " 5 . . . . . " + Environment.NewLine;

        // Act
        var output = CaptureConsoleOutput(() => { displayService.DisplayBoard(); });

        // Assert
        Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void DisplayService_DisplayBoard_PrintsBoard()
    {
        // Arrange
        var board = _boardProvider.Board;
        board.GetCellForCoordinates(0, 0).Status = CellStatus.Sunk;
        board.GetCellForCoordinates(0, 1).Status = CellStatus.Sunk;
        board.GetCellForCoordinates(0, 2).Status = CellStatus.Sunk;
        board.GetCellForCoordinates(0, 3).Status = CellStatus.Sunk;
        board.GetCellForCoordinates(1, 3).Status = CellStatus.Hit;
        board.GetCellForCoordinates(2, 3).Status = CellStatus.Hit;
        board.GetCellForCoordinates(3, 3).Status = CellStatus.Hit;
        board.GetCellForCoordinates(4, 4).Status = CellStatus.Miss;

        IDisplayService displayService = new DisplayService(_boardProvider);
        var expectedOutput =
            "   A B C D E " + Environment.NewLine +
            " 1 S . . . . " + Environment.NewLine +
            " 2 S . . . . " + Environment.NewLine +
            " 3 S . . . . " + Environment.NewLine +
            " 4 S X X X . " + Environment.NewLine +
            " 5 . . . . M " + Environment.NewLine;

        // Act
        var output = CaptureConsoleOutput(() => { displayService.DisplayBoard(); });

        // Assert
        Assert.Equal(expectedOutput, output);
    }

    [Theory]
    [InlineData(ShotResult.Miss, "Miss.")]
    [InlineData(ShotResult.Hit, "Hit. Battleship")]
    [InlineData(ShotResult.Sunk, "Sunk. Battleship")]
    public void DisplayService_DisplayShotResult_PrintsOutput(ShotResult shotResult, string expectedOutput)
    {
        // arrange
        IDisplayService displayService = new DisplayService(_boardProvider);
        var ship = new Ship("Battleship", 5);

        // act
        var output = CaptureConsoleOutput(() => { displayService.DisplayShotResult(shotResult, ship); });

        // assert
        Assert.Contains(expectedOutput, output);
    }

    [Fact]
    public void DisplayService_DisplayWelcomeMessage_PrintsOutput()
    {
        // arrange
        IDisplayService displayService = new DisplayService(_boardProvider);
        const string expectedOutput = "Welcome to Battleships!";

        // act
        var output = CaptureConsoleOutput(() => { displayService.DisplayWelcomeMessage(); });

        // assert
        Assert.Contains(expectedOutput, output);
    }

    [Fact]
    public void DisplayService_DisplayWonMessage_PrintsOutput()
    {
        // arrange
        IDisplayService displayService = new DisplayService(_boardProvider);
        const string expectedOutput = "Congratulation! You have won.";

        // act
        var output = CaptureConsoleOutput(() => { displayService.DisplayWonMessage(); });

        // assert
        Assert.Contains(expectedOutput, output);
    }

    [Fact]
    public void DisplayService_DisplayException_PrintsOutput()
    {
        // arrange
        IDisplayService displayService = new DisplayService(_boardProvider);
        var exception = new Exception("exampleMessage");

        // act
        var output = CaptureConsoleOutput(() => { displayService.DisplayException(exception); });

        // assert
        Assert.Contains(exception.Message, output);
    }

    private static string CaptureConsoleOutput(Action action)
    {
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        action.Invoke();
        return stringWriter.ToString();
    }
}