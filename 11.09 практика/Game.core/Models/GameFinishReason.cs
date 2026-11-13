namespace Game.Core.Models;

/// <summary>
/// Определяет причину завершения игры.
/// </summary>
public enum GameFinishReason
{
    Guessed, // угадал
    AttemptsEnded, // попытки закончились
    TimeExpired // время вышло
}