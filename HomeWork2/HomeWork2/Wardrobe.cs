namespace HomeWork2;
/// <summary>
/// Представляет шкаф.
/// </summary>
public class Wardrobe : Furniture
{

    /// <summary>
    /// Количество полок в шкафу.
    /// </summary>
    public int ShelvesCount { get; set; }

    /// <summary>
    /// Показывает, является ли шкаф вместительным.
    /// </summary>
    public bool IsSpacious
    {
        get
        {
            return ShelvesCount >= 5;

        }

    }

    /// <summary>
    /// Создаёт шкаф с заданными характеристиками.
    /// </summary>
    /// <param name="id">Идентификатор шкафа.</param>
    /// <param name="name">Название шкафа.</param>
    /// <param name="price">Цена шкафа.</param>
    /// <param name="shelvesCount">Количество полок.</param>
    public Wardrobe(int id,string name,decimal price,int shelvesCount)
        : base(id,name,price)
    {
        ShelvesCount = shelvesCount;
    }


    /// <summary>
    /// Выводит описание количества полок в шкафу.
    /// </summary>
    public override void Describe()
    {
        Console.WriteLine($"В шкафу {ShelvesCount} полок.");
    }

    /// <summary>
    /// Возвращает строковую информацию о шкафе.
    /// </summary>
    public override string ToString()
    {
        return $"[Wardrobe] {Name}, Price: {Price}, material: {Material}, shelves: {ShelvesCount}";
    }

}
