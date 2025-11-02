using HSEBank.Commands;
using HSEBank.Decorators;
using HSEBank.Domain.Models;
using HSEBank.Repositories;
using HSEBank.Services;

namespace HSEBank.Facades;

public class OperationFacade
{
    private readonly IOperationService _operations;
    private readonly IAccountRepository _accounts;
    private readonly CommandManager _commandManager;

    public OperationFacade(IOperationService ops, IAccountRepository accRepo, CommandManager mgr)
    {
        _operations = ops;
        _accounts = accRepo;
        _commandManager = mgr;
    }

    /// <summary>
    /// Выполнить операцию (доход/расход) через паттерн Команда, с возможностью Undo.
    /// </summary>
    public void ExecuteOperation(OperationType type, uint accountId, uint categoryId, uint amount, string description)
    {
        var cmd = new CreateOperationCommand(_operations, _accounts, type, accountId, categoryId, amount, description);
        var decorated = new CommandTimerDecorator(cmd, "CreateOperation");
        _commandManager.Execute(decorated);
    }

    /// <summary>
    /// Отменить последнюю операцию.
    /// </summary>
    public void UndoLastOperation()
    {
        _commandManager.Undo();
    }
}