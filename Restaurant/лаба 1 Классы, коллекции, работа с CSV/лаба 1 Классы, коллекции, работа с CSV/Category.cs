namespace лаба_1_Классы__коллекции__работа_с_CSV;

/// <summary>
/// класс представляющий категорию блюда
/// </summary>
public class Category
{

    /// <summary>
    /// свойство класса с уникальным номером категории
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    ///  свойство класса с названием категории
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    ///  свойство класса с типом категории
    /// </summary>
    public string Type { get; private set; }


    /// <summary>
    /// свойство, которое возвращает строку с инфой о категории
    /// </summary>
    public string Info
    {
        get
        {
            return $"{Name} - {Type}";
        }
    }

    //конструктор класса Category
    public Category(int id, string name, string type)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id категории должен быть больше нуля.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название категории не может быть пустым.", nameof(name));

        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Тип категории не может быть пустым.", nameof(type));

        Id = id;
        Name = name;
        Type = type;
    }
}
