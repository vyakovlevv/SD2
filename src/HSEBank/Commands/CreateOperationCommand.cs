using HSEBank.Domain.Models;
using HSEBank.Repositories;
using HSEBank.Services;

namespace HSEBank.Commands;

public class CreateOperationCommand : BaseCommand
{
    private readonly IOperationService _opService;
    private readonly IAccountRepository _accountRepo;
    private readonly OperationType _type;
    private readonly uint _accountId;
    private readonly uint _categoryId;
    private readonly uint _amount;
    private readonly string _desc;
    private Operation? _createdOp;

    public CreateOperationCommand(IOperationService opService, IAccountRepository accountRepo,
        OperationType type, uint accountId, uint categoryId, uint amount, string desc = "")
    {
        _opService = opService; 
        _accountRepo = accountRepo;
        _type = type; 
        _accountId = accountId; 
        _categoryId = categoryId; 
        _amount = amount; 
        _desc = desc;
    }

    public override void Execute()
    {
        var acc = _accountRepo.Get(_accountId);
        // улыбочку, вы в кадре
        CreateSnapshot(acc, null);

        _createdOp = _opService.Create(_type, _accountId, _categoryId, _amount, _desc);
    }

    public override void Undo()
    {
        if (_memento == null) throw new InvalidOperationException("Нет снимка");
        if (_createdOp == null) throw new InvalidOperationException("Нечего откатывать");

        // восстанавливаем состояние
        if (_memento.AccountSnapshot != null)
        {
            _accountRepo.Set(_memento.AccountSnapshot);
        }
        _opService.Remove(_createdOp.Id);
    }
}