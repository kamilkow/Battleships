using Battleships.ConsoleUI.Extensions;
using Battleships.Domain.Enums;
using Battleships.Domain.Models;
using Xunit;

namespace Battleships.Tests.ConsoleUI.Extensions;

public class CellExtensionsTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void CellExtensions_ToSymbol_ReturnsCorrectSymbol(Cell input, char output)
    {
        // Act
        var result = input.ToSymbol();

        // Assert
        Assert.Equal(output, result);
    }

    public static IEnumerable<object[]> TestData()
    {
        yield return new object[]
        {
            new Cell { Status = CellStatus.Miss },
            'M'
        };

        yield return new object[]
        {
            new Cell { Status = CellStatus.Hit },
            'X'
        };

        yield return new object[]
        {
            new Cell { Status = CellStatus.Sunk },
            'S'
        };

        yield return new object[]
        {
            new Cell { Status = CellStatus.Empty },
            '.'
        };

        yield return new object[]
        {
            new Cell { Status = CellStatus.Occupied },
            '.'
        };

        yield return new object[]
        {
            new Cell { Status = CellStatus.Invalid },
            '.'
        };
    }
}