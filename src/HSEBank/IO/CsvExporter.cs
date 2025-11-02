using HSEBank.Domain.Models;

namespace HSEBank.IO;

public class CsvExporter : DataExporter
{
    public override void Export(BankAccount account)
    {
        Lines.Add($"ACCOUNT;{account.Id};{account.Name};{account.Balance}");
    }

    public override void Export(Category category)
    {
        Lines.Add($"CATEGORY;{category.Id};{category.Type};{category.Name}");
    }

    public override void Export(Operation operation)
    {
        Lines.Add($"OPERATION;{operation.Id};{operation.Type};{operation.Amount};{operation.Date:o};{operation.CategoryId};{operation.AccountId};{operation.Description}");
    }

    protected override void SaveToFile(string path, string content)
    {
        File.WriteAllText(path, content);
        Console.WriteLine($"[Exporter] CSV сохранено в {path}");
    }
}