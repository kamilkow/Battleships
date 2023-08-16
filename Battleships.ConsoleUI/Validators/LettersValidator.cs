using System.Text.RegularExpressions;

namespace Battleships.ConsoleUI.Validators;

public static partial class LettersValidator
{
    [GeneratedRegex("^[a-zA-Z]([1-9]|1[0-9]|2[0-6])$")]
    private static partial Regex AllowedLetters();

    public static bool IsValidInput(string input) => AllowedLetters().IsMatch(input);
}