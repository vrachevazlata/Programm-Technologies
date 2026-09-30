namespace лаба_1_Классы__коллекции__работа_с_CSV;


/// <summary>
/// Класс, представляющий блюдо
/// </summary>
public class Dish
{
    /// <summary>
    /// свойство класса с уникальным номером блюда
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// свойство класса с названием блюда
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// свойство класса с уникальным номером повара
    /// </summary>
    public int ChefId { get; private set; }
    /// <summary>
    /// свойство класса с уникальным номером категории
    /// </summary>
    public int CategoryId { get; private set; }
    /// <summary>
    /// свойство класса с ценой блюда
    /// </summary>
    public decimal Price { get; private set; }
    /// <summary>
    /// свойство класса с весом блюда
    /// </summary>
    public int Weight { get; private set; }


    /// <summary>
    /// вычисляемое свойство, которое возвращает цену за грамм блюда
    /// </summary>
    public decimal PricePerGram   
    {
        get 
        { 
            return Price / Weight; 
        }
    }

    /// <summary>
    /// вычисляемое свойство, которое возвращает true, если вес больше 500 грамм
    /// </summary>
    public bool IsHeavy
    {
        get 
        { 
            return Weight > 500; 
        }
    }

    /// <summary>
    /// метод который возвращает строку с инфой о блюде
    /// </summary>
    public string GetInfo()
    {
        return $"{Name} ({Price} руб., {Weight} г.)";
    }

    /// <summary>
    /// конструктор класса Dish
    /// </summary>
    public Dish(
        int id,
        string name,
        int chefId,
        int categoryId,
        decimal price,
        int weight)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id блюда должен быть больше нуля.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название блюда не может быть пустым.", nameof(name));

        if (chefId <= 0)
            throw new ArgumentOutOfRangeException(nameof(chefId), "Id повара должен быть больше нуля.");

        if (categoryId <= 0)
            throw new ArgumentOutOfRangeException(nameof(categoryId), "Id категории должен быть больше нуля.");

        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Цена не может быть отрицательной.");

        if (weight <= 0)
            throw new ArgumentOutOfRangeException(nameof(weight), "Вес должен быть больше нуля.");

        Id = id;
        Name = name;
        ChefId = chefId;
        CategoryId = categoryId;
        Price = price;
        Weight = weight;
    }
}
