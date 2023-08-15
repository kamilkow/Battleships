using Battleships.Application.Services.Interfaces;
using Battleships.ConsoleUI.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships.ConsoleUI;

public static class Program
{
    public static void Main()
    {
        var startup = new Startup();
        var serviceProvider = startup.ConfigureServices();

        Run(serviceProvider);
    }

    private static void Run(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var shipPlacementService = scope.ServiceProvider.GetRequiredService<IShipPlacementService>();
        var gameEngineService = scope.ServiceProvider.GetRequiredService<IGameStateService>();
        var shootingService = scope.ServiceProvider.GetRequiredService<IShootingService>();
        var displayService = scope.ServiceProvider.GetRequiredService<IDisplayService>();
        var userInputService = scope.ServiceProvider.GetRequiredService<IUserInputService>();

        shipPlacementService.PlaceShipsOnBoard();
        displayService.DisplayWelcomeMessage();
        displayService.DisplayBoard();

        while (!gameEngineService.IsGameFinished())
        {
            try
            {
                var (col, row) = userInputService.GetCellCoordinates();
                var result = shootingService.ShootAtCoordinates(col, row);
                displayService.DisplayShotResult(result, shootingService.LastHitShip);
            }
            catch (Exception ex)
            {
                displayService.DisplayException(ex);
            }
            finally
            {
                displayService.DisplayBoard();
            }
        }

        displayService.DisplayWonMessage();
    }
}