namespace Game.Models;

/// <summary>
/// Определяет причину завершения игры.
/// </summary>
public enum GameFinishReason
{
    Guessed,
    AttemptsEnded,
    TimeExpired
}