using System.Reflection;

namespace HomeWork2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Chair chair = new Chair(1,"Stool",1500m,3);
            Table table = new Table(2,"Stol",3000m,7);
            Wardrobe wardrobe = new Wardrobe(3,"Shkaf",8000m,6);


            List<Furniture> furnitureList= new List<Furniture>();

            furnitureList.Add(table);
            furnitureList.Add(wardrobe);  
            furnitureList.Add(chair);
            foreach(Furniture obj in furnitureList)
            {
                Console.WriteLine("");
                Console.WriteLine($"Информация об объекте:\n{obj}");
                Console.WriteLine($"Цена с НДС 20%: {obj.PriceWithVat}");
                obj.Describe();
                if (obj is IAssemblable assemblable)
                {
                    assemblable.Assemble();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Этот предмет мебели нельзя собрать.\n");
                }
            }
        }
    }
}
