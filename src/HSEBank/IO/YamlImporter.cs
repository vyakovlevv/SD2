using HSEBank.Domain.Models;
using HSEBank.Repositories;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HSEBank.IO;

public class YamlImporter(
    IAccountRepository accountRepo,
    ICategoryRepository categoryRepo,
    IOperationRepository operationRepo)
    : DataImporter
{
    protected override IEnumerable<object> Parse(string raw)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var yamlObjects = deserializer.Deserialize<List<Dictionary<string, object>>>(raw);
        return yamlObjects;
    }

    protected override void ProcessItem(object item)
    {
        if (item is not Dictionary<string, object> dict) return;

        string? type = dict.GetValueOrDefault("model")?.ToString()?.ToLowerInvariant();
        if (type == null) return;

        switch (type)
        {
            case "account":
                var account = new BankAccount(
                    uint.Parse(dict.GetValueOrDefault("Id").ToString()),
                    dict.GetValueOrDefault("Name")?.ToString() ?? "Без имени",
                    uint.Parse(dict.GetValueOrDefault("Balance").ToString())
                );
                accountRepo.Set(account);
                break;

            case "category":
                Enum.TryParse<OperationType>(dict.GetValueOrDefault("Type")?.ToString() ?? "Expense", true,
                    out var catType);
                var category = new Category(uint.Parse(dict.GetValueOrDefault("Id").ToString()),
                    catType,
                    dict.GetValueOrDefault("Name")?.ToString() ?? "Без категории"
                );
                categoryRepo.Set(category);
                break;

            case "operation":
                Enum.TryParse<OperationType>(dict.GetValueOrDefault("Type")?.ToString() ?? "Expense", true,
                    out var opType);
                var op = new Operation(
                    uint.Parse(dict.GetValueOrDefault("Id").ToString()),
                    opType,
                    uint.Parse(dict.GetValueOrDefault("AccountId")?.ToString()),
                    uint.Parse(dict.GetValueOrDefault("CategoryId")?.ToString()),
                    uint.Parse(dict.GetValueOrDefault("Amount").ToString()),
                    DateTime.Parse(dict.GetValueOrDefault("Date")?.ToString() ?? DateTime.UtcNow.ToString()),
                    dict.GetValueOrDefault("Description")?.ToString() ?? ""
                );
                operationRepo.Set(op);
                break;

            default:
                Console.WriteLine($"[YamlImporter] Неизвестный тип: {type}");
                break;
        }
    }
}