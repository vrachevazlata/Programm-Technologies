using Game.Core.Interfaces;

namespace Game.Core.Models;

/// <summary>
/// Представляет настройки уровня сложности.
/// </summary>
public class DifficultyLevel : IDifficultyLevel
{
    public string Name { get; }
    public int MinNumber { get; }
    public int MaxNumber { get; }
    public int MaxAttempts { get; }
    public int TimeLimitSeconds { get; }


    public DifficultyLevel(string name, int minNumber, int maxNumber, int maxAttempts, int timeLimitSeconds)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Название сложности не может быть пустым.", nameof(name));
        }

        if (maxNumber <= minNumber)
        {
            throw new ArgumentException("Максимальное число должно быть больше минимального.");
        }

        if (maxAttempts <= 0 || timeLimitSeconds <= 0)
        {
            throw new ArgumentException("Количество попыток и время должны быть больше нуля.");
        }

        Name = name;
        MinNumber = minNumber;
        MaxNumber = maxNumber;
        MaxAttempts = maxAttempts;
        TimeLimitSeconds = timeLimitSeconds;
    }

    public override string ToString()
    {
        return $"{Name}: числа {MinNumber}–{MaxNumber}, попыток — {MaxAttempts}, время — {TimeLimitSeconds} сек.";
    }
}