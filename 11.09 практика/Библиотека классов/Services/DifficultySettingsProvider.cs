using Game.Core.Interfaces;
using Game.Core.Models;

namespace Game.Core.Services;

/// <summary>
/// Создаёт настройки стандартных и пользовательского уровней сложности.
/// </summary>
public class DifficultySettingsProvider : IDifficultySettingsProvider
{

    private readonly IReadOnlyList<IDifficultyLevel> _levels =
    new List<IDifficultyLevel>
    {
        new DifficultyLevel("Лёгкая", 1, 50, 10, 120),
        new DifficultyLevel("Средняя", 1, 100, 8, 90),
        new DifficultyLevel("Сложная", 1, 500, 9, 60),
        new DifficultyLevel("Экспертная", 1, 1000, 7, 45)
    }.AsReadOnly();

    public IReadOnlyList<IDifficultyLevel> Levels
    {
        get
        {
            return _levels;
        }
    }

    /// <summary>
    /// Возвращает настройки стандартного уровня сложности.
    /// </summary>

    public IDifficultyLevel CreateCustomLevel(int minNumber, int maxNumber, int maxAttempts, int timeLimitSeconds)
    {
        return new DifficultyLevel("Пользовательская",minNumber,maxNumber,maxAttempts,timeLimitSeconds);
    }
}