using HSEBank.Domain.Events;
using HSEBank.IO;

namespace HSEBank.Facades;

public class ImportFacade(
    CsvImporter csvImporter,
    JsonImporter jsonImporter,
    YamlImporter yamlImporter,
    IEventBus events)
{
    /// <summary>
    /// Импорт данных из файла, формат определяется автоматически по расширению.
    /// </summary>
    public void Import(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine($"Файл '{path}' не найден.");
            return;
        }

        string ext = Path.GetExtension(path).ToLowerInvariant();

        try
        {
            switch (ext)
            {
                case ".csv":
                    csvImporter.Import(path);
                    break;
                case ".json":
                    jsonImporter.Import(path);
                    break;
                case ".yaml":
                case ".yml":
                    yamlImporter.Import(path);
                    break;
                default:
                    Console.WriteLine($"Неподдерживаемый формат файла: {ext}");
                    return;
            }

            events.Publish(new DomainEvent("DataImported", path));
            Console.WriteLine($"Импорт завершён успешно ({ext})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при импорте: {ex.Message}");
        }
    }
}