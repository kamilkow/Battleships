using Battleships.Common.Providers.Interfaces;
using Battleships.ConsoleUI.Extensions;
using Battleships.ConsoleUI.Services.Interfaces;
using Battleships.Domain.Enums;
using Battleships.Domain.Models;

namespace Battleships.ConsoleUI.Services.Implementations;

public class DisplayService : IDisplayService
{
    private readonly IBoardProvider _boardProvider;

    public DisplayService(IBoardProvider boardProvider)
    {
        _boardProvider = boardProvider ?? throw new ArgumentNullException(nameof(boardProvider));
    }

    public void DisplayBoard()
    {
        var board = _boardProvider.Board;

        DisplayWrite(string.Empty.PadRight(3));
        for (var col = 0; col < board.Size; col++)
        {
            DisplayWrite($"{col.ToLetter()} ");
        }

        DisplayWriteLine();

        for (var row = 0; row < board.Size; row++)
        {
            DisplayWrite($"{row + 1,2} ");

            for (var col = 0; col < board.Size; col++)
            {
                var cell = board.GetCellForCoordinates(col, row);
                var cellSymbol = cell.ToSymbol();

                DisplayWrite($"{cellSymbol} ");
            }

            DisplayWriteLine();
        }
    }

    public void DisplayShotResult(ShotResult result, Ship? ship)
    {
        var output = result switch
        {
            ShotResult.Miss => "Miss.",
            ShotResult.Hit => $"Hit. {ship?.Name}.",
            ShotResult.Sunk => $"Sunk. {ship?.Name}.",
            _ => throw new ArgumentOutOfRangeException(nameof(result), result, null)
        };

        DisplayMessage(output);
    }

    public void DisplayWelcomeMessage() => DisplayMessage("Welcome to Battleships!");

    public void DisplayWonMessage() => DisplayMessage("Congratulation! You have won.");

    public void DisplayException(Exception exception) => DisplayMessage(exception.Message);

    public void DisplayMessage(string output) => DisplayWriteLine(Environment.NewLine + output + Environment.NewLine);

    private static void DisplayWriteLine(string? output = null) => Console.WriteLine(output);

    private static void DisplayWrite(string? output = null) => Console.Write(output);
}