using Battleships.Domain.Models;

namespace Battleships.Common.Providers.Interfaces;

public interface IShipsProvider
{
    public List<Ship> Ships { get; }
}