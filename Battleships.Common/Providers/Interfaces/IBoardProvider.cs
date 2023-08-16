using Battleships.Domain.Models;

namespace Battleships.Common.Providers.Interfaces;

public interface IBoardProvider
{
    public Board Board { get; }
}