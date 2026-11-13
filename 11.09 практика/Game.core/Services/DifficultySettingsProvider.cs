using Game.Core.Interfaces;
using Game.Core.Models;

namespace Game.Core.Services;

/// <summary>
/// Создаёт настройки стандартных и пользовательского уровней сложности.
/// </summary>
public class DifficultySettingsProvider : IDifficultySettingsProvider
{
    /// <summary>
    /// Возвращает настройки стандартного уровня сложности.
    /// </summary>
    public GameSettings GetSettings(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return new GameSettings(Difficulty.Easy, 1, 50, 10, 120);

            case Difficulty.Medium:
                return new GameSettings(Difficulty.Medium, 1, 100, 8, 90);

            case Difficulty.Hard:
                return new GameSettings(Difficulty.Hard, 1, 500, 9, 60);

            default:
                throw new ArgumentException("Для пользовательского режима необходимо указать собственные настройки.");
        }
    }


    public GameSettings CreateCustomSettings(int minNumber, int maxNumber, int maxAttempts, int timeLimitSeconds)
    {
        return new GameSettings(Difficulty.Custom, minNumber, maxNumber, maxAttempts, timeLimitSeconds);
    }
}