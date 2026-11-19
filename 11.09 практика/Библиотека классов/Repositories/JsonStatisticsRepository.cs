using System.Text.Json;
using System.Text.Json.Serialization;
using Game.Core.Interfaces;
using Game.Core.Models;

namespace Game.Core.Repositories;

/// <summary>
/// Загружает и сохраняет статистику в JSON-файле.
/// </summary>
public class JsonStatisticsRepository : IStatisticsRepository
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _serializerOptions;

    /// <summary>
    /// Создаёт репозиторий с указанным путём к файлу.
    /// </summary>
    public JsonStatisticsRepository(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("Путь к файлу не может быть пустым.", nameof(filePath));
        }

        _filePath = filePath;

        _serializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        _serializerOptions.Converters.Add(new JsonStringEnumConverter());
    }

    /// <summary>
    /// Загружает статистику из JSON-файла.
    /// </summary>
    public GameStatistics Load()
    {
        if (!File.Exists(_filePath))
        {
            return new GameStatistics();
        }

        string json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new GameStatistics();
        }

        try
        {
            GameStatistics? statistics = JsonSerializer.Deserialize<GameStatistics>(json, _serializerOptions);

            if (statistics == null)
            {
                return new GameStatistics();
            }

            return statistics;
        }
        catch (JsonException)
        {
            return new GameStatistics();
        }
    }

    /// <summary>
    /// Сохраняет статистику в JSON-файл.
    /// </summary>
    public void Save(GameStatistics statistics)
    {
        ArgumentNullException.ThrowIfNull(statistics);

        string json = JsonSerializer.Serialize(statistics, _serializerOptions);
        File.WriteAllText(_filePath, json);
    }
}