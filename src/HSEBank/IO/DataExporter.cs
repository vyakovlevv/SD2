using HSEBank.Domain.Models;

namespace HSEBank.IO;

public abstract class DataExporter : IExporter
{
    protected readonly List<string> Lines = new();

    public abstract void Export(BankAccount account);
    public abstract void Export(Category category);
    public abstract void Export(Operation operation);

    protected abstract void SaveToFile(string path, string content);

    public void Export(IEnumerable<object> objects, string path)
    {
        Lines.Clear();
        foreach (var obj in objects)
        {
            switch (obj)
            {
                case BankAccount a: a.AcceptExporter(this); break;
                case Category c: c.AcceptExporter(this); break;
                case Operation o: o.AcceptExporter(this); break;
            }
        }

        string content = string.Join(Environment.NewLine, Lines);
        SaveToFile(path, content);
    }
}