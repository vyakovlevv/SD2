using HSEBank.Domain.Models;

namespace HSEBank.Commands;

public class Memento
{
    public BankAccount? AccountSnapshot { get; set; }
    public Operation? OperationSnapshot { get; set; }
}