using HSEBank.IO;

namespace HSEBank.Domain.Models;

public class Operation : IUnique
{
    public uint Id { get; }
    public OperationType Type { get; private set; } 
    public OperationStatus Status { get; private set; }
    public uint AccountId { get; }      
    public uint Amount { get; private set; }     
    public DateTime Date { get; }      
    public string Description { get; private set; } 
    public uint CategoryId { get; }     
    
    
    public Operation(uint id, OperationType type, uint bankAccountId, uint categoryId, uint amount, DateTime date, string description = "")
    {
        Id = id;
        Type = type;
        AccountId = bankAccountId;
        CategoryId = categoryId;
        Amount = amount;
        Date = date;
        Description = description;
    }

    public void Update(uint amount, string description)
    {
        Amount = amount; 
        Description = description; 
    }
    
    public void UpdateStatus(OperationStatus status) => Status = status;
    
    public void AcceptExporter(IExporter exporter) => exporter.Export(this);

}