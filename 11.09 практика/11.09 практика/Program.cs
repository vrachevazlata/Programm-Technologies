using Game.Application;
using Game.Interfaces;
using Game.Models;
using Game.Repositories;
using Game.Services;

namespace Game;

internal class Program
{
    static void Main(string[] args)
    {
        IDifficultySettingsProvider settingsProvider = new DifficultySettingsProvider();
        GameSettings settings = settingsProvider.GetSettings(Difficulty.Easy);

        INumberGenerator numberGenerator = new RandomNumberGenerator();
        IStatisticsRepository statisticsRepository = new JsonStatisticsRepository("statistics.json");

        StatisticsService statisticsService = new StatisticsService(statisticsRepository);
        statisticsService.SetPlayerName("Тестовый игрок");

        GuessNumberGame game = new GuessNumberGame(settings, numberGenerator);
        ConsoleGameNotifier notifier = new ConsoleGameNotifier();

        notifier.Subscribe(game);
        statisticsService.Subscribe(game);

        game.Start();

        while (!game.IsFinished)
        {
            Console.WriteLine();
            Console.Write($"Введите число от {settings.MinNumber} до {settings.MaxNumber}: ");

            string? input = Console.ReadLine();

            if (game.CheckTimeLimit())
            {
                break;
            }

            if (!int.TryParse(input, out int number))
            {
                Console.WriteLine("Ошибка: необходимо ввести целое число.");
                continue;
            }

            if (number < settings.MinNumber || number > settings.MaxNumber)
            {
                Console.WriteLine($"Ошибка: число должно находиться в диапазоне от {settings.MinNumber} до {settings.MaxNumber}.");
                continue;
            }

            game.MakeGuess(number);
        }

        statisticsService.Unsubscribe(game);
        notifier.Unsubscribe(game);

        GameStatistics statistics = statisticsService.Statistics;

        Console.WriteLine();
        Console.WriteLine("ОБЩАЯ СТАТИСТИКА");
        Console.WriteLine($"Сыграно игр: {statistics.GamesPlayed}");
        Console.WriteLine($"Побед: {statistics.Wins}");
        Console.WriteLine($"Поражений: {statistics.Losses}");
        Console.WriteLine($"Процент побед: {statistics.WinRate:0.0}%");
        Console.WriteLine($"Среднее количество попыток: {statistics.AverageAttempts:0.0}");

        int bestEasyResult = statistics.GetBestAttempts(Difficulty.Easy);

        if (bestEasyResult > 0)
        {
            Console.WriteLine($"Лучший результат на лёгкой сложности: {bestEasyResult} попыток.");
        }
        else
        {
            Console.WriteLine("На лёгкой сложности пока нет побед.");
        }
    }
}