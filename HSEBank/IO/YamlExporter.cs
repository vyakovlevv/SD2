using HSEBank.Domain.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HSEBank.IO;

public class YamlExporter : DataExporter
    {
        private readonly List<object> _objects = new();

        public override void Export(BankAccount account)
        {
            _objects.Add(new
            {
                Model = "Account",
                account.Id,
                account.Name,
                account.Balance
            });
        }

        public override void Export(Category category)
        {
            _objects.Add(new
            {
                Model = "Category",
                category.Id,
                category.Type,
                category.Name
            });
        }

        public override void Export(Operation operation)
        {
            _objects.Add(new
            {
                Model = "Operation",
                operation.Id,
                operation.Type,
                operation.Amount,
                operation.Date,
                operation.CategoryId,
                operation.AccountId,
                operation.Description
            });
        }

        protected override void SaveToFile(string path, string content)
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var yaml = serializer.Serialize(_objects);

            File.WriteAllText(path, yaml);
            Console.WriteLine($"[Exporter] YAML export saved to {path}");
        }
        
    }