using HSEBank.Domain.Models;
using HSEBank.Repositories;
using HSEBank.Services;

namespace HSEBank.Commands;

public class CreateOperationCommand(
    IOperationService opService,
    IAccountRepository accountRepo,
    OperationType type,
    uint accountId,
    uint categoryId,
    uint amount,
    string desc = "")
    : BaseCommand
{
    private Operation? _createdOp;

    public override void Execute()
    {
        var acc = accountRepo.Get(accountId);
        // улыбочку, вы в кадре
        CreateSnapshot(acc.Clone(), null);

        _createdOp = opService.Create(type, accountId, categoryId, amount, desc);
    }

    public override void Undo()
    {
        if (_memento == null) throw new InvalidOperationException("Нет снимка");
        if (_createdOp == null) throw new InvalidOperationException("Нечего откатывать");

        // восстанавливаем состояние
        if (_memento.AccountSnapshot != null)
        {
            accountRepo.Set(_memento.AccountSnapshot);
        }
        opService.Remove(_createdOp.Id);
    }
}