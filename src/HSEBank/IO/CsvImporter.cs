using HSEBank.Domain.Models;
using HSEBank.Repositories;

namespace HSEBank.IO;

public class CsvImporter(
    IAccountRepository accountRepo,
    ICategoryRepository categoryRepo,
    IOperationRepository operationRepo)
    : DataImporter
{
    protected override IEnumerable<object> Parse(string raw)
    {
        var lines = raw.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);
        var items = new List<object>();

        foreach (var line in lines)
        {
            var parts = line.Trim().Split(';');
            if (parts.Length == 0) continue;

            string type = parts[0].Trim().ToUpperInvariant();
            items.Add(new { Type = type, Data = parts.Skip(1).ToArray() });
        }

        return items;
    }

    protected override void ProcessItem(object item)
    {
        dynamic dyn = item;

        string type = dyn.Type;
        string[] data = dyn.Data;

        switch (type)
        {
            case "ACCOUNT":
                ImportAccount(data);
                break;
            case "CATEGORY":
                ImportCategory(data);
                break;
            case "OPERATION":
                ImportOperation(data);
                break;
            default:
                Console.WriteLine($"[CsvImporter] Неизвестная строка: {type}");
                break;
        }
    }

    private void ImportAccount(string[] data)
    {
        try
        {
            var account = new BankAccount(
                uint.Parse(data[0]),
                data[1],
                uint.Parse(data[2])
            );

            accountRepo.Set(account);
            Console.WriteLine($"[CsvImporter] Импортирован счёт: {account.Name} ({account.Balance} rub)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CsvImporter] Ошибка импорта счёта: {ex.Message}");
        }
    }

    private void ImportCategory(string[] data)
    {
        try
        {
            Enum.TryParse<OperationType>(data[1], true, out var type);
            var category = new Category(uint.Parse(data[0]), type, data[2]);

            categoryRepo.Set(category);
            Console.WriteLine($"[CsvImporter] Импортирована категория: {category.Name} ({category.Type})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CsvImporter] Ошибка импорта категории: {ex.Message}");
        }
    }

    private void ImportOperation(string[] data)
    {
        try
        {
            Enum.TryParse<OperationType>(data[1], true, out var type);

            var op = new Operation(
                uint.Parse(data[0]),
                type,
                uint.Parse(data[5]),
                uint.Parse(data[4]),
                uint.Parse(data[2]),
                DateTime.Parse(data[3]),
                description: data.Length > 6 ? data[6] : ""
            );

            operationRepo.Set(op);
            Console.WriteLine($"[CsvImporter] Импортирована операция {op.Type} {op.Amount} rub от {op.Date}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CsvImporter] Ошибка импорта операции: {ex.Message}");
        }
    }
}