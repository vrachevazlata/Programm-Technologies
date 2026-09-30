namespace лаба_1_Классы__коллекции__работа_с_CSV;

/// <summary>
/// класс представляющий повара
/// </summary>
public class Chef
{
    /// <summary>
    ///  свойство класса с уникальным номером работника
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    ///  свойство класса с ФИО работника
    /// </summary>
    public string FullName { get; private set; }
    /// <summary>
    /// свойство класса со специальностью работника
    /// </summary>
    public string Specialty { get; private set; }


    /// <summary>
    /// вычисляемое свойство, которое возвращает true, если повар является шеф-поваром
    /// </summary>
    public bool IsChef => Specialty == "Шеф-повар";


    /// <summary>
    /// метод, который возвращает строку с инфу о поваре 
    /// </summary>

    public string GetInfo() => $"{FullName} ({Specialty})";


    /// <summary>
    /// конструктор класса Chef
    /// </summary>
    public Chef(int id, string fullName, string specialty)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id повара должен быть больше нуля.");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("ФИО повара не может быть пустым.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(specialty))
            throw new ArgumentException("Специальность не может быть пустой.", nameof(specialty));

        Id = id;
        FullName = fullName;
        Specialty = specialty;
    }

}
