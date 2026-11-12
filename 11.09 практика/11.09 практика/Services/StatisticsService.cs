using Game.Events;
using Game.Interfaces;
using Game.Models;

namespace Game.Services;

/// <summary>
/// Обрабатывает результаты игр и управляет статистикой.
/// </summary>
public class StatisticsService
{
    private readonly IStatisticsRepository _repository;
    private string _playerName = "Игрок";

    /// <summary>
    /// Текущая игровая статистика.
    /// </summary>
    public GameStatistics Statistics { get; }

    /// <summary>
    /// Создаёт сервис и загружает сохранённую статистику.
    /// </summary>
    public StatisticsService(IStatisticsRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
        Statistics = _repository.Load();
    }

    /// <summary>
    /// Устанавливает имя текущего игрока.
    /// </summary>
    public void SetPlayerName(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            throw new ArgumentException("Имя игрока не может быть пустым.", nameof(playerName));
        }

        _playerName = playerName.Trim();
    }

    /// <summary>
    /// Подписывается на события указанной игры.
    /// </summary>
    public void Subscribe(GuessNumberGame game)
    {
        ArgumentNullException.ThrowIfNull(game);
        game.GameFinished += OnGameFinished;
    }

    /// <summary>
    /// Отписывается от событий указанной игры.
    /// </summary>
    public void Unsubscribe(GuessNumberGame game)
    {
        ArgumentNullException.ThrowIfNull(game);
        game.GameFinished -= OnGameFinished;
    }

    /// <summary>
    /// Обрабатывает завершение игры и сохраняет результат.
    /// </summary>
    private void OnGameFinished(object? sender, GameFinishedEventArgs e)
    {
        GameRecord record = new GameRecord
        {
            PlayerName = _playerName,
            Difficulty = e.Difficulty,
            IsWon = e.Reason == GameFinishReason.Guessed,
            Attempts = e.Attempts,
            TimeSpentSeconds = e.TimeSpentSeconds,
            PlayedAt = DateTime.Now
        };

        Statistics.AddGame(record);
        _repository.Save(Statistics);
    }
}