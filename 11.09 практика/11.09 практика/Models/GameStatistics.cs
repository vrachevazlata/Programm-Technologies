using System.Text.Json.Serialization;

namespace Game.Models;

/// <summary>
/// Хранит историю игр и рассчитывает общую статистику.
/// </summary>
public class GameStatistics
{
    /// <summary>
    /// Список завершённых игр.
    /// </summary>
    public List<GameRecord> Games { get; set; } = new List<GameRecord>();

    /// <summary>
    /// Возвращает количество сыгранных игр.
    /// </summary>
    [JsonIgnore]
    public int GamesPlayed
    {
        get
        {
            return Games.Count;
        }
    }

    /// <summary>
    /// Возвращает количество побед.
    /// </summary>
    [JsonIgnore]
    public int Wins
    {
        get
        {
            int wins = 0;

            foreach (GameRecord game in Games)
            {
                if (game.IsWon)
                {
                    wins++;
                }
            }

            return wins;
        }
    }

    /// <summary>
    /// Возвращает количество поражений.
    /// </summary>
    [JsonIgnore]
    public int Losses
    {
        get
        {
            return GamesPlayed - Wins;
        }
    }

    /// <summary>
    /// Возвращает процент побед.
    /// </summary>
    [JsonIgnore]
    public double WinRate
    {
        get
        {
            if (GamesPlayed == 0)
            {
                return 0;
            }

            return (double)Wins / GamesPlayed * 100;
        }
    }

    /// <summary>
    /// Возвращает среднее количество попыток.
    /// </summary>
    [JsonIgnore]
    public double AverageAttempts
    {
        get
        {
            if (GamesPlayed == 0)
            {
                return 0;
            }

            int totalAttempts = 0;

            foreach (GameRecord game in Games)
            {
                totalAttempts += game.Attempts;
            }

            return (double)totalAttempts / GamesPlayed;
        }
    }

    /// <summary>
    /// Добавляет результат завершённой игры.
    /// </summary>
    public void AddGame(GameRecord game)
    {
        ArgumentNullException.ThrowIfNull(game);
        Games.Add(game);
    }

    /// <summary>
    /// Возвращает лучший результат для указанной сложности.
    /// </summary>
    public int GetBestAttempts(Difficulty difficulty)
    {
        int bestAttempts = 0;

        foreach (GameRecord game in Games)
        {
            if (game.Difficulty == difficulty && game.IsWon)
            {
                if (bestAttempts == 0 || game.Attempts < bestAttempts)
                {
                    bestAttempts = game.Attempts;
                }
            }
        }

        return bestAttempts;
    }
}