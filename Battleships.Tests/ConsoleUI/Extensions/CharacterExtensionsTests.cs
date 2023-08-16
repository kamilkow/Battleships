using Battleships.ConsoleUI.Extensions;
using Xunit;

namespace Battleships.Tests.ConsoleUI.Extensions;

public class CharacterExtensionsTests
{
    [Theory]
    [InlineData(0, 'A')]
    [InlineData(1, 'B')]
    [InlineData(2, 'C')]
    [InlineData(25, 'Z')]
    public void CharacterExtensions_ToLetter_ReturnsCorrectLetter(int input, char output)
    {
        // Act
        var result = input.ToLetter();

        // Assert
        Assert.Equal(output, result);
    }

    [Theory]
    [InlineData('A', 0)]
    [InlineData('B', 1)]
    [InlineData('C', 2)]
    [InlineData('Z', 25)]
    public void CharacterExtensions_ToNumber_ReturnsCorrectIndex(char input, int output)
    {
        // Act
        var result = input.ToNumber();

        // Assert
        Assert.Equal(output, result);
    }
}