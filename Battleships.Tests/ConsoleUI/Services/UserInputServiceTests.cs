using Battleships.Common.Exceptions;
using Battleships.Common.Providers.Interfaces;
using Battleships.ConsoleUI.Services.Implementations;
using Battleships.ConsoleUI.Services.Interfaces;
using Battleships.Domain.Models;
using NSubstitute;
using Xunit;

namespace Battleships.Tests.ConsoleUI.Services;

public class UserInputServiceTests
{
    private readonly IDisplayService _displayService = Substitute.For<IDisplayService>();
    private readonly IBoardProvider _boardProvider = Substitute.For<IBoardProvider>();

    private readonly IUserInputService _userInputService;

    public UserInputServiceTests()
    {
        var board = new Board(10);
        _boardProvider.Board.Returns(board);
        _userInputService = new UserInputService(_displayService, _boardProvider);
    }

    [Theory]
    [InlineData("A1")]
    [InlineData("B5")]
    [InlineData("d1")]
    [InlineData(" e10")]
    public void UserInputService_GetCellCoordinates_ReturnsCoordinates(string input)
    {
        // Arrange
        Console.SetIn(new StringReader(input));

        // Act
        var (col, row) = _userInputService.GetCellCoordinates();

        // Assert
        Assert.InRange(col, 0, 9);
        Assert.InRange(row, 0, 9);
    }

    [Theory]
    [InlineData("")]
    [InlineData("InvalidInput")]
    [InlineData("A0")]
    [InlineData("B11")]
    public void UserInputService_GetCellCoordinates_ThrowsUserInputException(string input)
    {
        // Arrange
        Console.SetIn(new StringReader(input));

        // Act & Assert
        Assert.Throws<UserInputException>(() => _userInputService.GetCellCoordinates());
    }
}