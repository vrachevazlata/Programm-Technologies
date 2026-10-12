namespace HomeWork2;

/// <summary>
/// Представляет базовую модель мебели.
/// </summary>
public abstract class Furniture
{
    private int _id;
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Material Material { get; set; } = Material.Wood;

    public decimal PriceWithVat {
        get
        {
            return Price * 1.2m;
        }
    }

    public Furniture(int id, string name, decimal price)
    {
        _id = id;
        Name = name;
        Price = price;
    }

    public Furniture(string name) : this(0, name, 0)
    {

    }


    /// <summary>
    /// Выводит описание предмета мебели.
    /// </summary>
    public abstract void Describe();
}


