namespace HomeWork2;

public class Wardrobe : Furniture
{
    public int ShelvesCount { get; set; }
    public bool IsSpacious
    {
        get
        {
            return ShelvesCount >= 5;

        }

    }

    public Wardrobe(int id,string name,decimal price,int shelvesCount)
        : base(id,name,price)
    {
        ShelvesCount = shelvesCount;
    }


    public override void Describe()
    {
        Console.WriteLine($"Wardrobe has {ShelvesCount} shelves.");
    }
    public override string ToString()
    {
        return $"[Wardrobe] {Name}, {Price}, material: {Material}, shelves: {ShelvesCount}";
    }

}
