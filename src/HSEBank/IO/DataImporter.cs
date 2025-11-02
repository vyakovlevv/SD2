namespace HSEBank.IO;

public abstract class DataImporter
{
    public void Import(string path)
    {
        var raw = ReadFile(path);
        var items = Parse(raw);
        foreach (var it in items) ProcessItem(it);
    }

    protected string ReadFile(string path)
    {
        return File.ReadAllText(path);
    }
    protected abstract IEnumerable<object> Parse(string raw);
    protected abstract void ProcessItem(object item);
}