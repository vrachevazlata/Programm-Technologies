namespace HomeWork2;

/// <summary>
/// Представляет стул.
/// </summary>
public class Chair : Furniture
{
    /// <summary>
    /// Количество ножек стула.
    /// </summary>
    public int LegsCount { get; set; }

    /// <summary>
    /// Показывает, является ли стул табуретом с тремя ножками.
    /// </summary>
    public bool IsStool
    {
        get
        {
            return LegsCount == 3;
        }
    }

    /// <summary>
    /// Создаёт стул с заданными характеристиками.
    /// </summary>
    /// <param name="id">Идентификатор стула.</param>
    /// <param name="name">Название стула.</param>
    /// <param name="price">Цена стула.</param>
    /// <param name="legsCount">Количество ножек.</param>
    public Chair(int id,string name, decimal price, int legsCount)
        : base(id,name,price)
    {
        LegsCount = legsCount;
    }

    /// <summary>
    /// Выводит описание стула.
    /// </summary>
    public override void Describe()
    {
        if (IsStool)
        {
            Console.WriteLine("Это табурет с 3 ножками.");
        }
        else
        {
            Console.WriteLine($"У стула {LegsCount} ножек.");
        }

    }

    /// <summary>
    /// Возвращает строковую информацию о стуле.
    /// </summary>
    public override string ToString()
    {
        return $"[Chair] {Name}, Price: {Price}, material: {Material}, legs: {LegsCount}";
    }
}
