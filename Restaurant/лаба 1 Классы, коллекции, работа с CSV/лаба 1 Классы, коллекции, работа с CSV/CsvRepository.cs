namespace лаба_1_Классы__коллекции__работа_с_CSV;

/// <summary>
/// класс, представляющий репозиторий данных из CSV файлов
/// </summary>
public class CsvRepository
{
    private string _basePath;

    /// <summary>
    /// конструктор класса, который принимает путь к папке с CSV файлами
    /// </summary>
    public CsvRepository(string basePath)
    {
        _basePath = basePath;
    }


    /// <summary>
    /// метод для получения списка поваров из CSV файла
    /// </summary>
    /// <returns></returns>
    public List<Chef> GetChefs()
    {
        List<Chef> result = new List<Chef>();

        string[] lines = File.ReadAllLines(Path.Combine(_basePath, "chefs.csv"));

        if (lines.Length < 2) return result;

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');

            if (parts.Length < 3) continue;

            Chef chef = new Chef(int.Parse(parts[0]), parts[1], parts[2]);

            result.Add(chef);
        }

        return result;
    }

    /// <summary>
    /// метод для получения списка категорий из CSV файла
    /// </summary>
    /// <returns></returns>

    public List<Category> GetCategories()
    {
        List<Category> result = new List<Category>();

        string[] lines = File.ReadAllLines(Path.Combine(_basePath, "categories.csv"));

        if (lines.Length < 2) return result;

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');

            if (parts.Length < 3)  continue;

            Category category = new Category(int.Parse(parts[0]), parts[1], parts[2]);

            result.Add(category);
        }

        return result;
    }

    /// <summary>
    /// метод для получения списка блюд из CSV файла
    /// </summary>
    /// <returns></returns>
    public List<Dish> GetDishes()
    {
        List<Dish> result = new List<Dish>();

        string[] lines = File.ReadAllLines(Path.Combine(_basePath, "dishes.csv"));

        if (lines.Length < 2) return result;

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');

            if (parts.Length < 6) continue;

            Dish dish = new Dish(int.Parse(parts[0]), parts[1], int.Parse(parts[2]), int.Parse(parts[3]), decimal.Parse(parts[4]), int.Parse(parts[5]));

            result.Add(dish);
        }

        return result;
    }
}

