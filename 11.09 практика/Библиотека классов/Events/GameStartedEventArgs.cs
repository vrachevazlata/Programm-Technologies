using Game.Core.Interfaces;

namespace Game.Core.Events;

/// <summary>
/// Содержит данные о начале игры.
/// </summary>
public class GameStartedEventArgs : EventArgs
{
    /// <summary>
    /// Уровень сложности начавшейся игры.
    /// </summary>
    public IDifficultyLevel Level { get; }

    /// <summary>
    /// Создаёт данные события начала игры.
    /// </summary>
    public GameStartedEventArgs(IDifficultyLevel level)
    {
        ArgumentNullException.ThrowIfNull(level);
        Level = level;
    }
}