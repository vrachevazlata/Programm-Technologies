namespace HomeWork2;

/// <summary>
/// Представляет стул.
/// </summary>
public class Chair : Furniture
{
    public int LegsCount { get; set; }

    public bool IsStool
    {
        get
        {
            return LegsCount == 3;
        }
    }

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
            Console.WriteLine("Chair is a stool.");
        }
        else
        {
            Console.WriteLine($"Chair has {LegsCount} legs.");
        }

    }

    /// <summary>
    /// Возвращает строковую информацию о стуле.
    /// </summary>
    public override string ToString()
    {
        return $"[Chair] {Name}, {Price}, material: {Material}, legs: {LegsCount}";
    }
}
