namespace лаба_1_Классы__коллекции__работа_с_CSV;

/// <summary>
/// класс, представляющий репозиторий данных в памяти
/// </summary>
internal class InMemoryRepository
{
    private List<Chef> _chefs;
    private List<Category> _categories;
    private List<Dish> _dishes;

    /// <summary>
    /// конструктор класса, который инициализирует данные в памяти
    /// </summary>
    public InMemoryRepository()
    {
        _chefs = new List<Chef>
        {
            new Chef(1, "Петрова А.А.", "Шеф-повар"),
            new Chef(2, "Иванов И.И.", "Повар"),
            new Chef(3, "Сидорова Е.В.", "Кондитер"),
            new Chef(4, "Смирнов Д.О.", "Повар"),
            new Chef(5, "Орлова М.С.", "Су-шеф")
        };

        _categories = new List<Category>
        {
            new Category(1, "Супы", "горячие блюда"),
            new Category(2, "Салаты", "холодные блюда"),
            new Category(3, "Десерты", "сладкие блюда"),
            new Category(4, "Гарниры", "горячие блюда"),
            new Category(5, "Напитки", "холодные напитки")
        };

        _dishes = new List<Dish>
        {
            new Dish(1, "Борщ", 1, 1, 350, 400),
            new Dish(2, "Окрошка", 1, 1, 250, 300),
            new Dish(3, "Солянка", 1, 1, 400, 350),
            new Dish(4, "Цезарь", 2, 2, 450, 250),
            new Dish(5, "Чизкейк", 3, 3, 300, 180),
            new Dish(6, "Картофельное пюре", 4, 4, 180, 200),
            new Dish(7, "Лимонад", 5, 5, 150, 500)
        };
    }



    /// <summary>
    /// методы для получения данных из репозитория
    /// </summary>
    public List<Chef> GetChefs()
    {
        return _chefs;
    }

    public List<Category> GetCategories()
    {
        return _categories;
    }

    public List<Dish> GetDishes()
    {
        return _dishes;
    }
}
