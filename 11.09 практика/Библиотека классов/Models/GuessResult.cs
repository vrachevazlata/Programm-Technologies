namespace Game.Core.Models;

/// <summary>
/// Определяет результат попытки игрока.
/// </summary>
public enum GuessResult
{
    SecretNumberIsHigher,
    SecretNumberIsLower,
    Correct,
    TimeExpired
}