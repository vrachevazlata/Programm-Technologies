using Game.Core.Models;

namespace Game.Core.Services;

/// <summary>
/// Определяет победителя многопользовательской игры.
/// </summary>
public class MultiplayerService
{
    /// <summary>
    /// Находит победителя среди результатов игроков.
    /// </summary>
    /// <param name="results">Результаты игроков.</param>
    /// <returns>Результат победителя или null, если никто не выиграл.</returns>
    public GameRecord? FindWinner(List<GameRecord> results)
    {
        ArgumentNullException.ThrowIfNull(results);

        GameRecord? winner = null;

        foreach (GameRecord result in results)
        {
            if (!result.IsWon)
            {
                continue;
            }

            if (winner == null)
            {
                winner = result;
                continue;
            }

            if (result.Attempts < winner.Attempts)
            {
                winner = result;
            }
            else if (result.Attempts == winner.Attempts &&
                     result.TimeSpentSeconds < winner.TimeSpentSeconds)
            {
                winner = result;
            }
        }

        return winner;
    }
}