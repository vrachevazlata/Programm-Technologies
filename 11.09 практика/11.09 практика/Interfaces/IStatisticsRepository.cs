using Game.Models;

namespace Game.Interfaces;

/// <summary>
/// Описывает загрузку и сохранение игровой статистики.
/// </summary>
public interface IStatisticsRepository
{
    /// <summary>
    /// Загружает статистику.
    /// </summary>
    GameStatistics Load();

    /// <summary>
    /// Сохраняет статистику.
    /// </summary>
    void Save(GameStatistics statistics);
}