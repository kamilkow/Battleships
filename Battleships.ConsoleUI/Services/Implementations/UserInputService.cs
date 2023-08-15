using Battleships.Common.Exceptions;
using Battleships.ConsoleUI.Extensions;
using Battleships.ConsoleUI.Services.Interfaces;
using Battleships.Common.Providers.Interfaces;
using Battleships.ConsoleUI.Validators;

namespace Battleships.ConsoleUI.Services.Implementations;

public class UserInputService : IUserInputService
{
    private readonly IDisplayService _displayService;
    private readonly IBoardProvider _boardProvider;

    public UserInputService(IDisplayService displayService, IBoardProvider boardProvider)
    {
        _displayService = displayService;
        _boardProvider = boardProvider;
    }

    public (int col, int row) GetCellCoordinates()
    {
        var (col, row) = GetAndValidateInput("Enter cell coordinates (e.g. A5)");
        return (col, row);
    }

    private (int col, int row) GetAndValidateInput(string message)
    {
        _displayService.DisplayMessage(message);
        var trimmedInput = Console.ReadLine()?.ToUpper().Trim();
        if (trimmedInput == null || !LettersValidator.IsValidInput(trimmedInput))
            throw new UserInputException("Incorrect input! Please provide correct value.");

        var col = trimmedInput[0].ToNumber();
        var row = int.Parse(trimmedInput[1..]) - 1;

        if (col >= _boardProvider.Board.Size || row >= _boardProvider.Board.Size)
            throw new UserInputException("Coordinates out of range! Please provide valid coordinates.");

        return (col, row);
    }
}