using HSEBank.Commands;
using HSEBank.Decorators;
using HSEBank.Domain.Models;
using HSEBank.Repositories;
using HSEBank.Services;

namespace HSEBank.Facades;

public class OperationFacade(IOperationService ops, IAccountRepository accRepo, CommandManager mgr)
{
    /// <summary>
    /// Выполнить операцию (доход/расход) через паттерн Команда, с возможностью Undo.
    /// </summary>
    public void ExecuteOperation(OperationType type, uint accountId, uint categoryId, uint amount, string description)
    {
        var cmd = new CreateOperationCommand(ops, accRepo, type, accountId, categoryId, amount, description);
        var decorated = new CommandTimerDecorator(cmd, "CreateOperation");
        mgr.Execute(decorated);
    }

    /// <summary>
    /// Отменить последнюю операцию.
    /// </summary>
    public void UndoLastOperation()
    {
        mgr.Undo();
    }
}