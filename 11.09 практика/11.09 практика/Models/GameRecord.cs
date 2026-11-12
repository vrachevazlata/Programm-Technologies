namespace Game.Models;

/// <summary>
/// Хранит результат одной завершённой игры.
/// </summary>
public class GameRecord
{
    /// <summary>
    /// Имя игрока.
    /// </summary>
    public string PlayerName { get; set; } = string.Empty;

    /// <summary>
    /// Уровень сложности.
    /// </summary>
    public Difficulty Difficulty { get; set; }

    /// <summary>
    /// Показывает, победил ли игрок.
    /// </summary>
    public bool IsWon { get; set; }

    /// <summary>
    /// Количество использованных попыток.
    /// </summary>
    public int Attempts { get; set; }

    /// <summary>
    /// Продолжительность игры в секундах.
    /// </summary>
    public int TimeSpentSeconds { get; set; }

    /// <summary>
    /// Дата и время завершения игры.
    /// </summary>
    public DateTime PlayedAt { get; set; }
}