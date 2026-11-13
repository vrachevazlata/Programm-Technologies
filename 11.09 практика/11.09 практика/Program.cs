using Game.Application;
using Game.Interfaces;
using Game.Repositories;
using Game.Services;

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

        ConsoleGameApplication application = new ConsoleGameApplication(numberGenerator, settingsProvider, statisticsService, notifier);

        application.Run();
    }
}