namespace HomeWork2;

/// <summary>
/// Представляет стол.
/// </summary>
public class Table : Furniture, IAssemblable
{
    /// <summary>
    /// Количество ножек стола.
    /// </summary>
    public int SeatsCount { get; set; }

    /// <summary>
    /// Показывает является ли стол большим.
    /// </summary>
    public bool IsBigTable
    {
        get
        {
            return SeatsCount >= 6;
        }
    }

    /// <summary>
    /// Создает стол с заданными характеристиками. 
    /// </summary>
    /// <param name="id">Индивидуальный код мебели.</param>
    /// <param name="name">Название мебели.</param>
    /// <param name="price">Цена мебели.</param>
    /// <param name="seatsCount">Количество ножек.</param>
    public Table(int id, string name, decimal price, int seatsCount)
        : base(id, name,price)
    {
        SeatsCount = seatsCount;
    }

    /// <summary>
    /// Выводит описание вместимости стола.
    /// </summary>
    public override void Describe()
    {
        Console.WriteLine($"Стол рассчитан на {SeatsCount} человек.");
    }

    /// <summary>
    /// Выполняет сборку стола.
    /// </summary>
    public void Assemble()
    {
        Console.WriteLine("Стол успешно собран.");
    }

    /// <summary>
    /// Возвращает строковую информацию о столе.
    /// </summary>
    public override string ToString()
    {
        return $"[Table] {Name}, Price: {Price}, material: {Material}, seats: {SeatsCount}";
    }
}
