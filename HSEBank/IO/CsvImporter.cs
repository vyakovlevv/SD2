namespace HSEBank.IO;

public class CsvImporter : DataImporter
{
    protected override IEnumerable<object> Parse(string raw)
    {
        // TODO: parse CSV and map to domain DTOs
        return new List<object>();
    }

    protected override void ProcessItem(object item)
    {
        // TODO: convert DTO to domain objects and store via repos/services
    }
}

public class JsonImporter : DataImporter
{
    protected override IEnumerable<object> Parse(string raw) { return Enumerable.Empty<object>(); }
    protected override void ProcessItem(object item) { /* TODO */ }
}