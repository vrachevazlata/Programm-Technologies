namespace лаба_1_Классы__коллекции__работа_с_CSV;

internal class Program
{
    static void Main()
    {
        List<Chef> chefs;
        List<Category> categories;
        List<Dish> dishes;

        for (; ; )
        {
            try
            {
                Console.WriteLine("Выберите источник данных:");
                Console.WriteLine("1 — данные из программы");
                Console.WriteLine("2 — данные из CSV-файлов");
                Console.Write("Ваш выбор: ");

                bool correctInput =
                    int.TryParse(Console.ReadLine(), out int choice);

                if (!correctInput)
                {
                    Console.WriteLine("Необходимо ввести число.");
                    return;
                }

                switch (choice)
                {
                    case 1:
                        InMemoryRepository memoryRepository =
                            new InMemoryRepository();

                        chefs = memoryRepository.GetChefs();
                        categories = memoryRepository.GetCategories();
                        dishes = memoryRepository.GetDishes();

                        Console.WriteLine(
                            "\nДанные загружены из программы.");
                        break;

                    case 2:
                        CsvRepository csvRepository =
                            new CsvRepository("data");

                        chefs = csvRepository.GetChefs();
                        categories = csvRepository.GetCategories();
                        dishes = csvRepository.GetDishes();

                        Console.WriteLine(
                            "\nДанные загружены из CSV-файлов.");
                        break;

                    default:
                        Console.WriteLine("Неверный выбор.");
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Произошла ошибка: {ex.Message}");
                return;
            }

            // 1. Поиск повара блюда
            Console.WriteLine("\n1. ПОИСК ПОВАРА БЛЮДА");

            Chef? foundChef = FindChef("Борщ", dishes, chefs);

            if (foundChef != null)
            {
                Console.WriteLine(
                    $"Повар блюда \"Борщ\": {foundChef.GetInfo()}");
            }
            else
            {
                Console.WriteLine("Повар блюда не найден.");
            }

            // 2. Поиск категории блюда
            Console.WriteLine("\n2. ПОИСК КАТЕГОРИИ БЛЮДА");

            Dish? selectedDish = null;

            foreach (Dish dish in dishes)
            {
                if (dish.Name == "Борщ")
                {
                    selectedDish = dish;
                    break;
                }
            }

            if (selectedDish != null)
            {
                Category? foundCategory =
                    FindCategory(selectedDish, categories);

                if (foundCategory != null)
                {
                    Console.WriteLine(
                        $"Категория блюда \"{selectedDish.Name}\": " +
                        foundCategory.Info);
                }
                else
                {
                    Console.WriteLine("Категория блюда не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Блюдо не найдено.");
            }

            // 3. Общий вес блюд
            Console.WriteLine("\n3. ОБЩИЙ ВЕС БЛЮД");

            int totalWeight = GetTotalWeight(dishes);

            Console.WriteLine(
                $"Общий вес всех блюд: {totalWeight} г");

            // 4. Блюда повара по возрастанию цены
            Console.WriteLine(
                "\n4. БЛЮДА ПОВАРА ПО ВОЗРАСТАНИЮ ЦЕНЫ");

            List<Dish> chefDishes =
                GetDishesByChefSortedByPrice(
                    "Петрова А.А.", chefs, dishes);

            if (chefDishes.Count == 0)
            {
                Console.WriteLine("Блюда повара не найдены.");
            }
            else
            {
                foreach (Dish dish in chefDishes)
                {
                    Console.WriteLine(dish.GetInfo());
                }
            }

            // 5. Вывод всех блюд
            Console.WriteLine("\n5. ВСЕ БЛЮДА");

            PrintAllDishes(dishes, chefs, categories);

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Находит повара по названию блюда.
    /// </summary>
    static Chef? FindChef(string dishName, List<Dish> dishes, List<Chef> chefs)
    {
        if (dishName == null)
            throw new ArgumentNullException(nameof(dishName));

        if (dishes == null)
            throw new ArgumentNullException(nameof(dishes));

        if (chefs == null)
            throw new ArgumentNullException(nameof(chefs));

        foreach (Dish dish in dishes)
        {
            if (dish.Name == dishName)
            {
                foreach (Chef chef in chefs)
                {
                    if (chef.Id == dish.ChefId)
                    {
                        return chef;
                    }
                }

                return null;
            }
        }

        return null;
    }

    /// <summary>
    /// Находит категорию указанного блюда.
    /// </summary>
    static Category? FindCategory(Dish dish, List<Category> categories)
    {

        if (dish == null)
            throw new ArgumentNullException(nameof(dish));

        if (categories == null)
            throw new ArgumentNullException(nameof(categories));

        foreach (Category category in categories)
        {
            if (category.Id == dish.CategoryId)
            {
                return category;
            }
        }

        return null;
    }

    /// <summary>
    /// Вычисляет общий вес всех блюд.
    /// </summary>
    static int GetTotalWeight(List<Dish> dishes)
    {
        if (dishes == null)
            throw new ArgumentNullException(nameof(dishes));

        int totalWeight = 0;

        foreach (Dish dish in dishes)
        {
            totalWeight += dish.Weight;
        }

        return totalWeight;
    }

    /// <summary>
    /// Возвращает блюда указанного повара,
    /// отсортированные по возрастанию цены.
    /// </summary>
    static List<Dish> GetDishesByChefSortedByPrice(string chefFullName, List<Chef> chefs, List<Dish> dishes)
    {
        if (chefFullName == null)
            throw new ArgumentNullException(nameof(chefFullName));

        if (chefs == null)
            throw new ArgumentNullException(nameof(chefs));

        if (dishes == null)
            throw new ArgumentNullException(nameof(dishes));

        List<Dish> result = new List<Dish>();

        Chef? foundChef = null;

        foreach (Chef chef in chefs)
        {
            if (chef.FullName == chefFullName)
            {
                foundChef = chef;
                break;
            }
        }

        if (foundChef == null)
        {
            return result;
        }


        foreach (Dish dish in dishes)
        {
            if (dish.ChefId == foundChef.Id)
            {
                result.Add(dish);
            }
        }



        for (int i = 0; i < result.Count - 1; i++)
        {
            for (int j = 0; j < result.Count - 1 - i; j++)
            {
                if (result[j].Price > result[j + 1].Price)
                {
                    Dish temp = result[j];
                    result[j] = result[j + 1];
                    result[j + 1] = temp;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Выводит все блюда вместе с поварами и категориями.
    /// </summary>
    static void PrintAllDishes(List<Dish> dishes, List<Chef> chefs, List<Category> categories)
    {
        if (dishes == null)
            throw new ArgumentNullException(nameof(dishes));

        if (chefs == null)
            throw new ArgumentNullException(nameof(chefs));

        if (categories == null)
            throw new ArgumentNullException(nameof(categories));

        foreach (Dish dish in dishes)
        {
            Chef? foundChef = null;
            Category? foundCategory = null;

            foreach (Chef chef in chefs)
            {
                if (chef.Id == dish.ChefId)
                {
                    foundChef = chef;
                    break;
                }
            }


            foreach (Category category in categories)
            {
                if (category.Id == dish.CategoryId)
                {
                    foundCategory = category;
                    break;
                }
            }

            string chefName = "—";
            string categoryName = "—";

            if (foundChef != null)
            {
                chefName = foundChef.FullName;
            }

            if (foundCategory != null)
            {
                categoryName = foundCategory.Name;
            }

            Console.WriteLine($"\"{dish.GetInfo()}\" — повар {chefName}, " + $"категория \"{categoryName}\"");
        }
    }
}


