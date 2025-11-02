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
                    uint.Parse(dict.GetValueOrDefault("id").ToString()),
                    dict.GetValueOrDefault("name")?.ToString() ?? "Без имени",
                    uint.Parse(dict.GetValueOrDefault("balance").ToString())
                );
                accountRepo.Set(account);
                break;

            case "category":
                Enum.TryParse<OperationType>(dict.GetValueOrDefault("type")?.ToString() ?? "Expense", true,
                    out var catType);
                var category = new Category(uint.Parse(dict.GetValueOrDefault("id").ToString()),
                    catType,
                    dict.GetValueOrDefault("name")?.ToString() ?? "Без категории"
                );
                categoryRepo.Set(category);
                break;

            case "operation":
                Enum.TryParse<OperationType>(dict.GetValueOrDefault("type")?.ToString() ?? "Expense", true,
                    out var opType);
                var op = new Operation(
                    uint.Parse(dict.GetValueOrDefault("id").ToString()),
                    opType,
                    uint.Parse(dict.GetValueOrDefault("accountId")?.ToString()),
                    uint.Parse(dict.GetValueOrDefault("categoryId")?.ToString()),
                    uint.Parse(dict.GetValueOrDefault("amount").ToString()),
                    DateTime.Parse(dict.GetValueOrDefault("date")?.ToString() ?? DateTime.UtcNow.ToString()),
                    dict.GetValueOrDefault("description")?.ToString() ?? ""
                );
                operationRepo.Set(op);
                break;

            default:
                Console.WriteLine($"[YamlImporter] Неизвестный тип: {type}");
                break;
        }
    }
}