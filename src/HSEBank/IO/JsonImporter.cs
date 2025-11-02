using System.Text.Json;
using HSEBank.Domain.Models;
using HSEBank.Repositories;

namespace HSEBank.IO;

public class JsonImporter(
    IAccountRepository accountRepo,
    ICategoryRepository categoryRepo,
    IOperationRepository operationRepo)
    : DataImporter
{
    protected override IEnumerable<object> Parse(string raw)
    {
        try
        {
            var jsonObjects = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(raw);
            return jsonObjects ?? new List<Dictionary<string, object>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[JsonImporter] Ошибка парсинга JSON: {ex.Message}");
            return new List<object>();
        }
    }

    protected override void ProcessItem(object item)
    {
        if (item is not Dictionary<string, object> dict) return;

        string? type = dict.GetValueOrDefault("Model")?.ToString()?.ToLowerInvariant();
        if (type == null) return;

        try
        {
            switch (type)
            {
                case "account":
                    ImportAccount(dict);
                    break;
                case "category":
                    ImportCategory(dict);
                    break;
                case "operation":
                    ImportOperation(dict);
                    break;
                default:
                    Console.WriteLine($"[JsonImporter] Неизвестный тип: {type}");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[JsonImporter] Ошибка при обработке {type}: {ex.Message}");
        }
    }

    private void ImportAccount(Dictionary<string, object> dict)
    {
        var acc = new BankAccount(
            uint.Parse(dict.GetValueOrDefault("Id")?.ToString()),
            dict.GetValueOrDefault("Name")?.ToString() ?? "Без имени",
            uint.Parse(dict.GetValueOrDefault("Balance").ToString())
        );

        accountRepo.Set(acc);
        Console.WriteLine($"[JsonImporter] Импортирован счёт: {acc.Name} ({acc.Balance}₽)");
    }

    private void ImportCategory(Dictionary<string, object> dict)
    {
        Enum.TryParse<OperationType>(dict.GetValueOrDefault("Type")?.ToString() ?? "Expense", true, out var catType);

        var cat = new Category(uint.Parse(dict.GetValueOrDefault("Id")?.ToString()), catType,
            dict.GetValueOrDefault("Name")?.ToString() ?? "Без категории");

        categoryRepo.Set(cat);
        Console.WriteLine($"[JsonImporter] Импортирована категория: {cat.Name} ({cat.Type})");
    }

    private void ImportOperation(Dictionary<string, object> dict)
    {
        Enum.TryParse<OperationType>(dict.GetValueOrDefault("Type")?.ToString() ?? "Expense", true, out var opType);

        var op = new Operation(
            uint.Parse(dict.GetValueOrDefault("Id")?.ToString()),
            opType,
            uint.Parse(dict.GetValueOrDefault("AccountId")?.ToString()),
            uint.Parse(dict.GetValueOrDefault("CategoryId")?.ToString() ),
            uint.Parse(dict.GetValueOrDefault("Amount").ToString()),
            DateTime.Parse(dict.GetValueOrDefault("Date")?.ToString() ?? DateTime.UtcNow.ToString()),
            dict.GetValueOrDefault("Description")?.ToString() ?? ""
        );

        operationRepo.Set(op);
        Console.WriteLine($"[JsonImporter] Импортирована операция {op.Type} {op.Amount} rub от {op.Date}");
    }
}