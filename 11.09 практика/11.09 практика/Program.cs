using Game.Application;
using Game.Interfaces;
using Game.Models;
using Game.Services;

namespace Game;

internal class Program
{
    static void Main(string[] args)
    {
        IDifficultySettingsProvider settingsProvider = new DifficultySettingsProvider();
        GameSettings settings = settingsProvider.GetSettings(Difficulty.Easy);

        INumberGenerator numberGenerator = new RandomNumberGenerator();
        GuessNumberGame game = new GuessNumberGame(settings, numberGenerator);

        ConsoleGameNotifier notifier = new ConsoleGameNotifier();

        notifier.Subscribe(game);

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

        notifier.Unsubscribe(game);
    }
}