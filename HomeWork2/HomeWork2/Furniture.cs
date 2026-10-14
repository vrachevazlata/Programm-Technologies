namespace HomeWork2;

/// <summary>
/// Представляет базовую модель мебели.
/// </summary>
public abstract class Furniture
{
    private int _id;

    /// <summary>
    /// Название предмета мебели.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Цена предмета мебели.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Материал предмета мебели.
    /// </summary>
    public Material Material { get; set; } = Material.Wood;


    public decimal PriceWithVat {
        get
        {
            return Price * 1.2m;
        }
    }

    /// <summary>
    /// Конструктор, создаёт предмет мебели с указанными идентификатором, названием и ценой.
    /// </summary>
    /// <param name="id"> Уникальный код мебели.</param>
    /// <param name="name">Название мебели.</param>
    /// <param name="price">Цена мебели.</param>

    public Furniture(int id, string name, decimal price)
    {
        _id = id;
        Name = name;
        Price = price;
    }


    /// <summary>
    /// Конструктор, создаёт предмет мебели с указанным названием, нулевыми идентификатором и ценой.
    /// </summary>
    /// <param name="name">Название мебели.</param>
    public Furniture(string name) : this(0, name, 0)
    {

    }


    /// <summary>
    /// Выводит описание предмета мебели.
    /// </summary>
    public abstract void Describe();
}


