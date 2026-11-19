namespace Game.Core.Interfaces;

/// <summary>
/// Описывает уровень сложности игры.
/// </summary>
public interface IDifficultyLevel
{
    string Name { get; }
    int MinNumber { get; }
    int MaxNumber { get; }
    int MaxAttempts { get; }
    int TimeLimitSeconds { get; }
}