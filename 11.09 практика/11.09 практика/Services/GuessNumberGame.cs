using Game.Interfaces;
using Game.Models;

namespace Game.Services;

/// <summary>
/// Управляет одним раундом игры «Угадай число».
/// </summary>
public class GuessNumberGame
{
    private readonly GameSettings _settings;
    private readonly INumberGenerator _numberGenerator;

    private int _secretNumber;
    private DateTime _startTime;
    private bool _hasStarted;

    /// <summary>
    /// Количество использованных попыток.
    /// </summary>
    public int Attempts { get; private set; }

    /// <summary>
    /// Показывает, закончена ли игра.
    /// </summary>
    public bool IsFinished { get; private set; }

    /// <summary>
    /// Содержит причину завершения игры.
    /// </summary>
    public GameFinishReason? FinishReason { get; private set; }

    /// <summary>
    /// Возвращает количество оставшихся попыток.
    /// </summary>
    public int RemainingAttempts
    {
        get
        {
            return Math.Max(0, _settings.MaxAttempts - Attempts);
        }
    }

    /// <summary>
    /// Возвращает оставшееся время в секундах.
    /// </summary>
    public int RemainingSeconds
    {
        get
        {
            if (!_hasStarted)
            {
                return _settings.TimeLimitSeconds;
            }

            int elapsedSeconds = (int)(DateTime.UtcNow - _startTime).TotalSeconds;
            return Math.Max(0, _settings.TimeLimitSeconds - elapsedSeconds);
        }
    }

    /// <summary>
    /// Показывает загаданное число только после окончания игры.
    /// </summary>
    public int? SecretNumber
    {
        get
        {
            if (IsFinished)
            {
                return _secretNumber;
            }

            return null;
        }
    }

    /// <summary>
    /// Создаёт игру с указанными настройками и генератором числа.
    /// </summary>
    public GuessNumberGame(GameSettings settings, INumberGenerator numberGenerator)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(numberGenerator);

        _settings = settings;
        _numberGenerator = numberGenerator;
    }

    /// <summary>
    /// Начинает новый игровой раунд.
    /// </summary>
    public void Start()
    {
        _secretNumber = _numberGenerator.Generate(_settings.MinNumber, _settings.MaxNumber);
        _startTime = DateTime.UtcNow;
        Attempts = 0;
        IsFinished = false;
        FinishReason = null;
        _hasStarted = true;
    }

    /// <summary>
    /// Проверяет число, введённое игроком.
    /// </summary>
    public GuessResult MakeGuess(int number)
    {
        if (!_hasStarted)
        {
            throw new InvalidOperationException("Сначала необходимо запустить игру.");
        }

        if (IsFinished)
        {
            throw new InvalidOperationException("Игра уже закончена.");
        }

        if (CheckTimeLimit())
        {
            return GuessResult.TimeExpired;
        }

        if (number < _settings.MinNumber || number > _settings.MaxNumber)
        {
            throw new ArgumentOutOfRangeException(nameof(number), $"Число должно находиться в диапазоне от {_settings.MinNumber} до {_settings.MaxNumber}.");
        }

        Attempts++;

        if (number == _secretNumber)
        {
            FinishGame(GameFinishReason.Guessed);
            return GuessResult.Correct;
        }

        GuessResult result;

        if (number < _secretNumber)
        {
            result = GuessResult.SecretNumberIsHigher;
        }
        else
        {
            result = GuessResult.SecretNumberIsLower;
        }

        if (Attempts >= _settings.MaxAttempts)
        {
            FinishGame(GameFinishReason.AttemptsEnded);
        }

        return result;
    }

    /// <summary>
    /// Проверяет, закончилось ли время игры.
    /// </summary>
    public bool CheckTimeLimit()
    {
        if (!_hasStarted || IsFinished)
        {
            return FinishReason == GameFinishReason.TimeExpired;
        }

        if (RemainingSeconds > 0)
        {
            return false;
        }

        FinishGame(GameFinishReason.TimeExpired);
        return true;
    }

    /// <summary>
    /// Завершает игру по указанной причине.
    /// </summary>
    private void FinishGame(GameFinishReason reason)
    {
        IsFinished = true;
        FinishReason = reason;
    }
}