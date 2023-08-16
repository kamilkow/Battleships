using Battleships.Common.Configuration;
using Battleships.Common.Providers.Interfaces;
using Battleships.Domain.Models;
using Microsoft.Extensions.Options;

namespace Battleships.Common.Providers.Implementations;

public class BoardProvider : IBoardProvider
{
    public BoardProvider(IOptions<GameOptions> gameOptions)
    {
        Board = new Board(gameOptions.Value.BoardSize) ?? throw new ArgumentNullException(nameof(gameOptions));
    }

    public Board Board { get; }
}