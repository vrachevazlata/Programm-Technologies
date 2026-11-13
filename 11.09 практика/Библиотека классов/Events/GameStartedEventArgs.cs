using Game.Core.Models;

namespace Game.Core.Events;

/// <summary>
/// Содержит данные о начале игры.
/// </summary>
public class GameStartedEventArgs : EventArgs
{
    /// <summary>
    /// Настройки начавшейся игры.
    /// </summary>
    public GameSettings Settings { get; }

    /// <summary>
    /// Создаёт данные события начала игры.
    /// </summary>
    public GameStartedEventArgs(GameSettings settings)
    {
        Settings = settings;
    }
}