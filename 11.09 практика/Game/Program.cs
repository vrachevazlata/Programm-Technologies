using Game.Application;
using Game.Core.Interfaces;
using Game.Core.Repositories;
using Game.Core.Services;

namespace Game;

internal class Program
{
    static void Main(string[] args)
    {
        INumberGenerator numberGenerator = new RandomNumberGenerator();
        IDifficultySettingsProvider settingsProvider = new DifficultySettingsProvider();

        string statisticsPath = Path.Combine(AppContext.BaseDirectory, "statistics.json");
        IStatisticsRepository statisticsRepository = new JsonStatisticsRepository(statisticsPath);

        StatisticsService statisticsService = new StatisticsService(statisticsRepository);
        ConsoleGameNotifier notifier = new ConsoleGameNotifier();
        MultiplayerService multiplayerService = new MultiplayerService();

        ConsoleGameApplication application = new ConsoleGameApplication(numberGenerator, settingsProvider, statisticsService, notifier, multiplayerService);

        application.Run();
    }
}