namespace Game.Core.Models;

/// <summary>
/// Хранит настройки одного игрового режима.
/// </summary>
public class GameSettings
{
    /// <summary>
    /// Выбранный уровень сложности.
    /// </summary>
    public Difficulty Difficulty { get; }

    /// <summary>
    /// Минимальное возможное загаданное число.
    /// </summary>
    public int MinNumber { get; }

    /// <summary>
    /// Максимальное возможное загаданное число.
    /// </summary>
    public int MaxNumber { get; }

    /// <summary>
    /// Максимальное количество попыток.
    /// </summary>
    public int MaxAttempts { get; }

    /// <summary>
    /// Ограничение времени в секундах.
    /// </summary>
    public int TimeLimitSeconds { get; }

    /// <summary>
    /// Создаёт настройки игры.
    /// </summary>
    public GameSettings(Difficulty difficulty, int minNumber, int maxNumber, int maxAttempts, int timeLimitSeconds)
    {
        if (minNumber >= maxNumber)
        {
            throw new ArgumentException("Минимальное число должно быть меньше максимального.");
        }

        if (maxAttempts <= 0)
        {
            throw new ArgumentException("Количество попыток должно быть больше нуля.");
        }

        if (timeLimitSeconds <= 0)
        {
            throw new ArgumentException("Ограничение времени должно быть больше нуля.");
        }

        Difficulty = difficulty;
        MinNumber = minNumber;
        MaxNumber = maxNumber;
        MaxAttempts = maxAttempts;
        TimeLimitSeconds = timeLimitSeconds;
    }
}