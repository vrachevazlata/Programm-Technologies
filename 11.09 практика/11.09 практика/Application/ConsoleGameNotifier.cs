using Game.Events;
using Game.Models;
using Game.Services;

namespace Game.Application;

/// <summary>
/// Выводит в консоль уведомления о событиях игры.
/// </summary>
public class ConsoleGameNotifier
{
    /// <summary>
    /// Подписывается на события указанной игры.
    /// </summary>
    public void Subscribe(GuessNumberGame game)
    {
        ArgumentNullException.ThrowIfNull(game);

        game.GameStarted += OnGameStarted;
        game.GuessMade += OnGuessMade;
        game.GameFinished += OnGameFinished;
    }

    /// <summary>
    /// Отписывается от событий указанной игры.
    /// </summary>
    public void Unsubscribe(GuessNumberGame game)
    {
        ArgumentNullException.ThrowIfNull(game);

        game.GameStarted -= OnGameStarted;
        game.GuessMade -= OnGuessMade;
        game.GameFinished -= OnGameFinished;
    }

    /// <summary>
    /// Обрабатывает событие начала игры.
    /// </summary>
    private void OnGameStarted(object? sender, GameStartedEventArgs e)
    {
        Console.WriteLine("Игра «Угадай число»");
        Console.WriteLine($"Уровень сложности: {e.Settings.Difficulty}.");
        Console.WriteLine($"Диапазон: от {e.Settings.MinNumber} до {e.Settings.MaxNumber}.");
        Console.WriteLine($"Количество попыток: {e.Settings.MaxAttempts}.");
        Console.WriteLine($"Ограничение времени: {e.Settings.TimeLimitSeconds} секунд.");
    }

    /// <summary>
    /// Обрабатывает событие попытки игрока.
    /// </summary>
    private void OnGuessMade(object? sender, GuessMadeEventArgs e)
    {
        Console.WriteLine($"Попытка №{e.AttemptNumber}: {e.Number}");

        switch (e.Result)
        {
            case GuessResult.SecretNumberIsHigher:
                Console.WriteLine("Загаданное число больше.");
                break;

            case GuessResult.SecretNumberIsLower:
                Console.WriteLine("Загаданное число меньше.");
                break;

            case GuessResult.Correct:
                Console.WriteLine("Вы угадали число!");
                break;
        }

        if (e.Result != GuessResult.Correct)
        {
            Console.WriteLine($"Осталось попыток: {e.RemainingAttempts}.");
            Console.WriteLine($"Осталось времени: {e.RemainingSeconds} секунд.");
        }
    }

    /// <summary>
    /// Обрабатывает событие завершения игры.
    /// </summary>
    private void OnGameFinished(object? sender, GameFinishedEventArgs e)
    {
        Console.WriteLine();

        switch (e.Reason)
        {
            case GameFinishReason.Guessed:
                Console.WriteLine($"Победа! Использовано попыток: {e.Attempts}.");
                break;

            case GameFinishReason.AttemptsEnded:
                Console.WriteLine("Попытки закончились. Вы проиграли.");
                break;

            case GameFinishReason.TimeExpired:
                Console.WriteLine("Время закончилось. Вы проиграли.");
                break;
        }

        Console.WriteLine($"Загаданное число: {e.SecretNumber}.");
        Console.WriteLine($"Продолжительность игры: {e.TimeSpentSeconds} секунд.");
    }
}