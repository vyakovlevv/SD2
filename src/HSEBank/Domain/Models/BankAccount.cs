using HSEBank.IO;

namespace HSEBank.Domain.Models;

public class BankAccount : IUnique
{
    public uint Id { get; set; }
    public string Name { get; private set; }
    public uint Balance { get; private set; }
    
    
    public BankAccount(uint id, string name, uint initialBalance = 0)
    {
        Id = id;
        Name = name;
        Balance = initialBalance;
    }

    public void Deposit(uint amount)
    {
        Balance += amount;
    }

    public void Withdraw(uint amount)
    {
        if (amount > Balance) throw new InvalidOperationException("too big withdraw");
        Balance -= amount;
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        Name = name;
    }

    public void AcceptExporter(IExporter exporter) => exporter.Export(this);

    public BankAccount Clone()
    {
        return new BankAccount(Id, Name, Balance);
    }
}