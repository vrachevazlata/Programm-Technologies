using Game.Core.Interfaces;
using Game.Core.Models;
using Game.Core.Services;
using System.Text;

namespace Game.Application;

/// <summary>
/// Управляет консольным меню и запуском игровых раундов.
/// </summary>
public class ConsoleGameApplication
{
    private readonly INumberGenerator _numberGenerator;
    private readonly IDifficultySettingsProvider _settingsProvider;
    private readonly StatisticsService _statisticsService;
    private readonly ConsoleGameNotifier _notifier;
    private readonly MultiplayerService _multiplayerService;

    /// <summary>
    /// Создаёт консольное приложение с необходимыми зависимостями.
    /// </summary>
    public ConsoleGameApplication(INumberGenerator numberGenerator, IDifficultySettingsProvider settingsProvider, StatisticsService statisticsService, ConsoleGameNotifier notifier, MultiplayerService multiplayerService)
    {
        ArgumentNullException.ThrowIfNull(numberGenerator);
        ArgumentNullException.ThrowIfNull(settingsProvider);
        ArgumentNullException.ThrowIfNull(statisticsService);
        ArgumentNullException.ThrowIfNull(notifier);
        ArgumentNullException.ThrowIfNull(multiplayerService);

        _numberGenerator = numberGenerator;
        _settingsProvider = settingsProvider;
        _statisticsService = statisticsService;
        _notifier = notifier;
        _multiplayerService = multiplayerService;
    }

    /// <summary>
    /// Запускает главное меню приложения.
    /// </summary>
    public void Run()
    {
        Console.WriteLine("ИГРА «УГАДАЙ ЧИСЛО»");
        Console.WriteLine();

        string playerName = ReadPlayerName();
        _statisticsService.SetPlayerName(playerName);

        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("ГЛАВНОЕ МЕНЮ");
            Console.WriteLine("1 — Одиночная игра");
            Console.WriteLine("2 — Многопользовательская игра");
            Console.WriteLine("3 — Посмотреть статистику");
            Console.WriteLine("0 — Выход");
            Console.Write("Ваш выбор: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _statisticsService.SetPlayerName(playerName);
                    PlayGame();
                    break;

                case "2":
                    PlayMultiplayerGame();
                    break;

                case "3":
                    ShowStatistics();
                    break;

                case "0":
                    isRunning = false;
                    Console.WriteLine("Игра завершена.");
                    break;

                default:
                    Console.WriteLine("Ошибка: выберите пункт от 0 до 3.");
                    break;
            }
        }
    }

    /// <summary>
    /// Запрашивает имя игрока.
    /// </summary>
    private string ReadPlayerName()
    {
        while (true)
        {
            Console.Write("Введите имя игрока: ");
            string? playerName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(playerName))
            {
                return playerName.Trim();
            }

            Console.WriteLine("Имя игрока не может быть пустым.");
        }
    }

    /// <summary>
    /// Запрашивает уровень сложности и возвращает настройки игры.
    /// </summary>
    private IDifficultyLevel SelectDifficultyLevel()
    {
        Console.WriteLine("Выберите уровень сложности:");

        for (int i = 0; i < _settingsProvider.Levels.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_settingsProvider.Levels[i]}");
        }

        int customOption = _settingsProvider.Levels.Count + 1;
        Console.WriteLine($"{customOption}. Пользовательская");

        int choice = ReadInteger(
            "Ваш выбор: ",
            1,
            customOption);

        if (choice == customOption)
        {
            return ReadCustomLevel();
        }

        return _settingsProvider.Levels[choice - 1];
    }

    /// <summary>
    /// Запрашивает пользовательские настройки игры.
    /// </summary>
    private IDifficultyLevel ReadCustomLevel()
    {
        int minNumber = ReadInteger(
            "Минимальное число: ",
            int.MinValue,
            int.MaxValue);

        int maxNumber;

        do
        {
            maxNumber = ReadInteger("Максимальное число: ",int.MinValue,int.MaxValue);

            if (maxNumber <= minNumber)
            {
                Console.WriteLine("Максимальное число должно быть больше минимального.");
            }
        }
        while (maxNumber <= minNumber);

        int maxAttempts = ReadPositiveInteger("Количество попыток: ");
        int timeLimitSeconds = ReadPositiveInteger("Ограничение времени в секундах: ");

        return _settingsProvider.CreateCustomLevel(
            minNumber,
            maxNumber,
            maxAttempts,
            timeLimitSeconds);
    }


    /// <summary>
    /// Запускает одиночную игру.
    /// </summary>
    private void PlayGame()
    {
        IDifficultyLevel settings = SelectDifficultyLevel();
        PlayRound(settings);
    }

    /// <summary>
    /// Проводит один игровой раунд с указанными настройками.
    /// </summary>
    private GameRecord PlayRound(IDifficultyLevel settings)
    {
        GuessNumberGame game = new GuessNumberGame(settings, _numberGenerator);

        _notifier.Subscribe(game);
        _statisticsService.Subscribe(game);

        try
        {
            Console.WriteLine();
            game.Start();

            while (!game.IsFinished)
            {
                Console.WriteLine();
                Console.Write($"Введите число от {settings.MinNumber} до {settings.MaxNumber}: ");

                string? input = ReadLineUntilGameEnds(game);

                if (input == null || game.CheckTimeLimit())
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
        }
        finally
        {
            _statisticsService.Unsubscribe(game);
            _notifier.Unsubscribe(game);
        }

        int lastRecordIndex = _statisticsService.Statistics.Games.Count - 1;
        return _statisticsService.Statistics.Games[lastRecordIndex];
    }

    /// <summary>
    /// Запускает многопользовательскую игру.
    /// </summary>
    private void PlayMultiplayerGame()
    {
        Console.WriteLine();
        Console.WriteLine("МНОГОПОЛЬЗОВАТЕЛЬСКАЯ ИГРА");

        int playersCount;

        while (true)
        {
            playersCount = ReadPositiveInteger("Количество игроков от 2 до 10: ");

            if (playersCount >= 2 && playersCount <= 10)
            {
                break;
            }

            Console.WriteLine("Количество игроков должно быть от 2 до 10.");
        }

        List<string> playerNames = new List<string>();

        for (int i = 0; i < playersCount; i++)
        {
            Console.WriteLine();
            Console.WriteLine($"Игрок #{i + 1}");

            string playerName = ReadPlayerName();
            playerNames.Add(playerName);
        }

        IDifficultyLevel settings = SelectDifficultyLevel();
        List<GameRecord> results = new List<GameRecord>();

        for (int i = 0; i < playerNames.Count; i++)
        {
            string playerName = playerNames[i];

            Console.WriteLine();
            Console.WriteLine(" ");
            Console.WriteLine($"ХОД ИГРОКА: {playerName}");
            Console.WriteLine(" ");

            _statisticsService.SetPlayerName(playerName);

            GameRecord result = PlayRound(settings);
            results.Add(result);
        }

        Console.WriteLine();
        Console.WriteLine("РЕЗУЛЬТАТЫ МНОГОПОЛЬЗОВАТЕЛЬСКОЙ ИГРЫ");

        foreach (GameRecord result in results)
        {
            string resultText;

            if (result.IsWon)
            {
                resultText = "победа";
            }
            else
            {
                resultText = "поражение";
            }

            Console.WriteLine($"{result.PlayerName}: {resultText}, попыток — {result.Attempts}, время — {result.TimeSpentSeconds} сек.");
        }

        GameRecord? winner = _multiplayerService.FindWinner(results);

        Console.WriteLine();

        if (winner == null)
        {
            Console.WriteLine("Победителя нет: никто не угадал число.");
        }
        else
        {
            Console.WriteLine($"ПОБЕДИТЕЛЬ: {winner.PlayerName}");
            Console.WriteLine($"Попыток: {winner.Attempts}, время: {winner.TimeSpentSeconds} сек.");
        }
    }

    /// <summary>
    /// Показывает сохранённую игровую статистику.
    /// </summary>
    private void ShowStatistics()
    {
        GameStatistics statistics = _statisticsService.Statistics;

        Console.WriteLine();
        Console.WriteLine("СТАТИСТИКА");

        if (statistics.GamesPlayed == 0)
        {
            Console.WriteLine("Пока не сыграно ни одной игры.");
            return;
        }

        Console.WriteLine($"Сыграно игр: {statistics.GamesPlayed}");
        Console.WriteLine($"Побед: {statistics.Wins}");
        Console.WriteLine($"Поражений: {statistics.Losses}");
        Console.WriteLine($"Процент побед: {statistics.WinRate:0.0}%");
        Console.WriteLine($"Среднее количество попыток: {statistics.AverageAttempts:0.0}");

        Console.WriteLine();
        Console.WriteLine("ЛУЧШИЕ РЕЗУЛЬТАТЫ");

        foreach (IDifficultyLevel level in _settingsProvider.Levels)
        {
            ShowBestResult(level.Name);
        }

        ShowBestResult("Пользовательская");

        Console.WriteLine();
        Console.WriteLine("ПОСЛЕДНИЕ ИГРЫ");

        int firstGameIndex = Math.Max(0, statistics.Games.Count - 5);

        for (int i = firstGameIndex; i < statistics.Games.Count; i++)
        {
            GameRecord record = statistics.Games[i];

            string result;

            if (record.IsWon)
            {
                result = "Победа";
            }
            else
            {
                result = "Поражение";
            }

            Console.WriteLine($"{record.PlayedAt:g} | {record.PlayerName} | {record.Difficulty} | {result} | попыток: {record.Attempts} | время: {record.TimeSpentSeconds} сек.");
        }
    }

    /// <summary>
    /// Показывает лучший результат выбранного уровня сложности.
    /// </summary>
    private void ShowBestResult(string difficultyName)
    {
        int? bestAttempts =
            _statisticsService.Statistics.GetBestAttempts(difficultyName);

        if (bestAttempts.HasValue)
        {
            Console.WriteLine(
                $"{difficultyName}: лучший результат — {bestAttempts.Value} попыток.");
        }
        else
        {
            Console.WriteLine($"{difficultyName}: побед пока нет.");
        }
    }

    /// <summary>
    /// Читает строку, пока игровое время не закончилось.
    /// </summary>
    private string? ReadLineUntilGameEnds(GuessNumberGame game)
    {
        StringBuilder input = new StringBuilder();

        while (!game.IsFinished)
        {
            if (game.RemainingSeconds <= 0)
            {
                Console.WriteLine();
                game.CheckTimeLimit();
                return null;
            }

            if (!Console.KeyAvailable)
            {
                Thread.Sleep(50);
                continue;
            }

            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return input.ToString();
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                    Console.Write("\b \b");
                }

                continue;
            }

            if (!char.IsControl(key.KeyChar))
            {
                input.Append(key.KeyChar);
                Console.Write(key.KeyChar);
            }
        }

        return null;
    }

    /// <summary>
    /// Запрашивает целое число.
    /// </summary>
    private int ReadInteger(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int number))
            {
                return number;
            }

            Console.WriteLine("Ошибка: необходимо ввести целое число.");
        }
    }


    /// <summary>
    /// Запрашивает целое число в указанном диапазоне.
    /// </summary>
    private int ReadInteger(string message, int minValue, int maxValue)
    {
        while (true)
        {
            int number = ReadInteger(message);

            if (number >= minValue && number <= maxValue)
            {
                return number;
            }

            Console.WriteLine(
                $"Число должно находиться в диапазоне от {minValue} до {maxValue}.");
        }
    }

    /// <summary>
    /// Запрашивает положительное целое число.
    /// </summary>
    private int ReadPositiveInteger(string message)
    {
        while (true)
        {
            int number = ReadInteger(message);

            if (number > 0)
            {
                return number;
            }

            Console.WriteLine("Число должно быть больше нуля.");
        }
    }
}