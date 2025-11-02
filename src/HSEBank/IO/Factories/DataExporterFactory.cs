using Microsoft.Extensions.DependencyInjection;

namespace HSEBank.IO.Factories;

public class DataExporterFactory : IDataExporterFactory
{
    private readonly IServiceProvider _sp;

    public DataExporterFactory(IServiceProvider sp)
    {
        _sp = sp;
    }

    public DataExporter CreateExporter(string format)
    {
        return format.ToLower() switch
        {
            "csv" => _sp.GetRequiredService<CsvExporter>(),
            "json" => _sp.GetRequiredService<JsonExporter>(),
            "yaml" => _sp.GetRequiredService<YamlExporter>(),
            _ => throw new ArgumentException($"Unknown export format: {format}")
        };
    }
}