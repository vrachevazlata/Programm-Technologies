using Game.Core.Models;

namespace Game.Core.Interfaces;

/// <summary>
/// Описывает получение настроек выбранного уровня сложности.
/// </summary>
public interface IDifficultySettingsProvider
{
    /// <summary>
    /// Доступные стандартные уровни сложности.
    /// </summary>
    IReadOnlyList<IDifficultyLevel> Levels { get; }

    /// <summary>
    /// Создаёт пользовательский уровень сложности.
    /// </summary>
    IDifficultyLevel CreateCustomLevel(int minNumber, int maxNumber, int maxAttempts, int timeLimitSeconds);
}