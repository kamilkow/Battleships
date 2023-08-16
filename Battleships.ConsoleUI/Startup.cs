using Battleships.Application.Services.Implementations;
using Battleships.Application.Services.Interfaces;
using Battleships.Common.Configuration;
using Battleships.Common.Providers.Implementations;
using Battleships.Common.Providers.Interfaces;
using Battleships.ConsoleUI.Services.Implementations;
using Battleships.ConsoleUI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships.ConsoleUI;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public IServiceProvider ConfigureServices()
    {
        var serviceProvider = new ServiceCollection()
            .Configure<GameOptions>(options => Configuration.GetSection(nameof(GameOptions)).Bind(options))
            .AddScoped<IBoardProvider, BoardProvider>()
            .AddScoped<IShipsProvider, ShipsProvider>()
            .AddScoped<IShipPlacementService, ShipPlacementService>()
            .AddScoped<IShootingService, ShootingService>()
            .AddScoped<IGameStateService, GameStateService>()
            .AddScoped<IDisplayService, DisplayService>()
            .AddScoped<IUserInputService, UserInputService>()
            .BuildServiceProvider();

        return serviceProvider;
    }
}