using HSEBank.Facades;

namespace HSEBank.UI.Menu.Items;

public class ImportExportMenuItem : IMenuItem
{
    private readonly ImportFacade _import;
    private readonly ExportFacade _export;

    public ImportExportMenuItem(ImportFacade import, ExportFacade export)
    {
        _import = import;
        _export = export;
    }

    public string Title => "Импорт / Экспорт данных";

    public void Execute()
    {
        Console.WriteLine("1. Импорт\n2. Экспорт");
        var choice = Console.ReadLine();

        Console.Write("Введите путь к файлу (например, data.json): ");
        var path = Console.ReadLine() ?? "data.json";
        try
        {
            if (choice == "1")
            {
                _import.Import(path);
            }
            else
            {
                var ext = Path.GetExtension(path).TrimStart('.');
                _export.ExportAllData(ext, path);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Не получилось конвертировать данные: " + ex.Message);
        }
        
    }
}