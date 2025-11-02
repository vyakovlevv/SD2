namespace HSEBank.IO.Factories;

public interface IDataExporterFactory
{
    DataExporter CreateExporter(string format);
}