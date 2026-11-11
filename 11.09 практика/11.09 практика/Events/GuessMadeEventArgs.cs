using Game.Models;

namespace Game.Events;

/// <summary>
/// Содержит данные о попытке игрока.
/// </summary>
public class GuessMadeEventArgs : EventArgs
{
    /// <summary>
    /// Число, введённое игроком.
    /// </summary>
    public int Number { get; }

    /// <summary>
    /// Результат попытки.
    /// </summary>
    public GuessResult Result { get; }

    /// <summary>
    /// Номер использованной попытки.
    /// </summary>
    public int AttemptNumber { get; }

    /// <summary>
    /// Количество оставшихся попыток.
    /// </summary>
    public int RemainingAttempts { get; }

    /// <summary>
    /// Количество оставшихся секунд.
    /// </summary>
    public int RemainingSeconds { get; }

    /// <summary>
    /// Создаёт данные события попытки игрока.
    /// </summary>
    public GuessMadeEventArgs(int number, GuessResult result, int attemptNumber, int remainingAttempts, int remainingSeconds)
    {
        Number = number;
        Result = result;
        AttemptNumber = attemptNumber;
        RemainingAttempts = remainingAttempts;
        RemainingSeconds = remainingSeconds;
    }
}