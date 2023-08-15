using Battleships.Domain.Enums;
using Battleships.Domain.Models;

namespace Battleships.Application.Services.Interfaces;

public interface IGameStateService
{
    public bool IsGameFinished();
}