namespace HomeWork2;

/// <summary>
/// Представляет стол.
/// </summary>
public class Table : Furniture, IAssemblable
{
    public int SeatsCount { get; set; }

    public bool IsBigTable
    {
        get
        {
            return SeatsCount >= 6;
        }
    }

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
        Console.WriteLine($"Table seats {SeatsCount} people.");
    }

    /// <summary>
    /// Выполняет сборку стола.
    /// </summary>
    public void Assemble()
    {
        Console.WriteLine("Table assembled successfully.");
    }

    public override string ToString()
    {
        return $"[Table] {Name}, {Price}, material: {Material}, seats: {SeatsCount}";
    }
}
