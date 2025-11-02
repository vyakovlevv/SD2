using HSEBank.IO;

namespace HSEBank.Domain.Models;

public class Category : IUnique
{
    public uint Id { get; }
    public string Name { get; set; }
    public OperationType Type { get; set; }
    
    
    public Category(uint id, OperationType type, string name)
    {
        Id = id;
        Type = type; 
        Name = name;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Name required");
        Name = newName;
    }
    
    public void AcceptExporter(IExporter exporter) => exporter.Export(this);

}