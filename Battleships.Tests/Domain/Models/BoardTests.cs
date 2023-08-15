using Battleships.Domain.Models;
using Xunit;

namespace Battleships.Tests.Domain.Models
{
    public class BoardTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        public void Board_HasCorrectDimensions(int boardSize)
        {
            // act
            var gameBoard = new Board(boardSize);

            // assert
            Assert.Equal(boardSize, gameBoard.Cells.GetLength(0));
            Assert.Equal(boardSize, gameBoard.Cells.GetLength(1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Board_ThrowsException(int boardSize)
        {
            // act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Board(boardSize));
        }
    }
}