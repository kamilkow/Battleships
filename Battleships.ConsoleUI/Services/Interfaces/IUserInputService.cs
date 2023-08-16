namespace Battleships.ConsoleUI.Services.Interfaces;

public interface IUserInputService
{
    public (int col, int row) GetCellCoordinates();
}