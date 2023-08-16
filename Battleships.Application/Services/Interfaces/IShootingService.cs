using Battleships.Domain.Enums;
using Battleships.Domain.Models;

namespace Battleships.Application.Services.Interfaces;

public interface IShootingService
{
    public Ship? LastHitShip { get; }

    public ShotResult ShootAtCoordinates(int col, int row);
}