namespace Battleships.ConsoleUI.Extensions;

public static class CharacterExtensions
{
    private const char LetterOffset = 'A';

    public static char ToLetter(this int value) => (char)(value + LetterOffset);

    public static int ToNumber(this char value) => value - LetterOffset;
}