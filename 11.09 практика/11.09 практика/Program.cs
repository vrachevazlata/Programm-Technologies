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

        Console.WriteLine($"Сложность: {settings.Difficulty}");
        Console.WriteLine($"Диапазон: от {settings.MinNumber} до {settings.MaxNumber}");
        Console.WriteLine($"Попыток: {settings.MaxAttempts}");
        Console.WriteLine($"Время: {settings.TimeLimitSeconds} секунд");
    }
}