using Game.Interfaces;

namespace Game.Services;

/// <summary>
/// Генерирует случайные целые числа.
/// </summary>
public class RandomNumberGenerator : INumberGenerator
{
    /// <summary>
    /// Генерирует случайное число в указанном диапазоне включительно.
    /// </summary>
    public int Generate(int minNumber, int maxNumber)
    {
        if (minNumber >= maxNumber)
        {
            throw new ArgumentException("Минимальное число должно быть меньше максимального.");
        }

        return Random.Shared.Next(minNumber, maxNumber + 1);
    }
}