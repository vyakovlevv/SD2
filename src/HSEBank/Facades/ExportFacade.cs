using HSEBank.Domain.Events;
using HSEBank.IO;
using HSEBank.IO.Factories;
using HSEBank.Repositories;

namespace HSEBank.Facades;

public class ExportFacade
{
    private readonly IAccountRepository _accounts;
    private readonly ICategoryRepository _categories;
    private readonly IOperationRepository _operations;
    private readonly IDataExporterFactory _factory;

    public ExportFacade(IAccountRepository acc, ICategoryRepository cat, IOperationRepository ops,
        IDataExporterFactory factory)
    {
        _accounts = acc;
        _categories = cat;
        _operations = ops;
        _factory = factory;
    }

    public void ExportAllData(string format, string path)
    {
        var exporter = _factory.CreateExporter(format);
        var all = new List<object>();
        all.AddRange(_accounts.GetAll());
        all.AddRange(_categories.GetAll());
        all.AddRange(_operations.GetAll());

        exporter.Export(all, path);
        Console.WriteLine($"[ExportFacade] Данные экспортированы в формате {format.ToUpper()} -> {path}");
    }
}
