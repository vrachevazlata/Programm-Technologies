using Game.Core.Models;

namespace Game.Core.Events;

/// <summary>
/// Содержит данные о завершении игры.
/// </summary>
public class GameFinishedEventArgs : EventArgs
{
    /// <summary>
    /// Причина завершения игры.
    /// </summary>
    public GameFinishReason Reason { get; }

    /// <summary>
    /// Загаданное число.
    /// </summary>
    public int SecretNumber { get; }

    /// <summary>
    /// Количество использованных попыток.
    /// </summary>
    public int Attempts { get; }

    /// <summary>
    /// Количество потраченных секунд.
    /// </summary>
    public int TimeSpentSeconds { get; }

    /// <summary>
    /// Уровень сложности завершённой игры.
    /// </summary>
    public Difficulty Difficulty { get; }

    /// <summary>
    /// Создаёт данные события завершения игры.
    /// </summary>
    public GameFinishedEventArgs(GameFinishReason reason, int secretNumber, int attempts, int timeSpentSeconds, Difficulty difficulty)
    {
        Reason = reason;
        SecretNumber = secretNumber;
        Attempts = attempts;
        TimeSpentSeconds = timeSpentSeconds;
        Difficulty = difficulty;
    }
}