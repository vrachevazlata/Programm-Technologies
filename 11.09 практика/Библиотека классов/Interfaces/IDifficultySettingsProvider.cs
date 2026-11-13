using Game.Core.Models;

namespace Game.Core.Interfaces;

/// <summary>
/// Описывает получение настроек выбранного уровня сложности.
/// </summary>
public interface IDifficultySettingsProvider
{
    /// <summary>
    /// Возвращает готовые настройки стандартного уровня сложности.
    /// </summary>
    GameSettings GetSettings(Difficulty difficulty);

    /// <summary>
    /// Создаёт пользовательские настройки.
    /// </summary>
    GameSettings CreateCustomSettings(int minNumber, int maxNumber, int maxAttempts, int timeLimitSeconds);
}