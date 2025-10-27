using HSEBank.Domain.Models;

namespace HSEBank.Commands;

public abstract class BaseCommand : ICommand
{
    protected Memento? _memento;
    public abstract void Execute();
    public abstract void Undo();
    protected Memento CreateSnapshot(BankAccount? acc, Operation? op)
    {
        var m = new Memento
        {
            AccountSnapshot = acc,
            OperationSnapshot = op
        };
        _memento = m; 
        return m;
    }
}