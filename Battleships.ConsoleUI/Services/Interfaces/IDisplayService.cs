using Battleships.Domain.Enums;
using Battleships.Domain.Models;

namespace Battleships.ConsoleUI.Services.Interfaces;

public interface IDisplayService
{
    public void DisplayBoard();

    public void DisplayShotResult(ShotResult result, Ship? ship);

    public void DisplayWelcomeMessage();

    public void DisplayWonMessage();

    public void DisplayException(Exception exception);

    public void DisplayMessage(string output);
}