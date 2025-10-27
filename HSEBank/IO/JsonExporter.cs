using System.Text.Json;
using HSEBank.Domain.Models;

namespace HSEBank.IO;

public class JsonExporter : DataExporter
{
    private readonly List<object> _objects = new();

    public override void Export(BankAccount account) => _objects.Add(new {
        Model = "Account", account.Id, account.Name, account.Balance
    });

    public override void Export(Category category) => _objects.Add(new {
        Model = "Category", category.Id, category.Type, category.Name
    });

    public override void Export(Operation operation) => _objects.Add(new {
        Model = "Operation", operation.Id, operation.Type, operation.Amount,
        operation.Date, operation.CategoryId, operation.AccountId, operation.Description
    });

    protected override void SaveToFile(string path, string content)
    {
        var json = JsonSerializer.Serialize(_objects, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
        Console.WriteLine($"[Exporter] JSON сохранен в {path}");
    }
}