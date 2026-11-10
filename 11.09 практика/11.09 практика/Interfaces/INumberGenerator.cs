namespace Game.Interfaces;

/// <summary>
/// Описывает генератор целых чисел.
/// </summary>
public interface INumberGenerator
{
    /// <summary>
    /// Генерирует число в указанном диапазоне.
    /// </summary>
    int Generate(int minNumber, int maxNumber);
}