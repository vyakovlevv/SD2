using HSEBank.Domain.Events;
using HSEBank.Domain.Factories;
using HSEBank.Domain.Handlers;
using HSEBank.Domain.Models;
using HSEBank.Repositories;

namespace HSEBank.Services;

public class OperationService(
    IOperationRepository operationRepository,
    IAccountRepository accountRepository,
    IDomainFactory domainFactory,
    IEventBus events,
    OperationHandler handlerChain) : IOperationService
{
    public Operation Create(OperationType type, uint bankAccountId, uint categoryId, uint amount, string desc)
    {
        var op = domainFactory.CreateOperation(type, bankAccountId, categoryId, amount, desc);

        if (!handlerChain.Handle(op))
        {
            events.Publish(new DomainEvent("OperationFail", op));
            throw new InvalidOperationException("Операция отклонена в ходе проверки хэндлерами");

        }

        var account = accountRepository.Get(bankAccountId) ?? throw new InvalidOperationException("Аккаунт не найден");
        if (type == OperationType.Income)
        {
            account.Deposit(amount);
        }
        else
        {
            account.Withdraw(amount);
        }

        accountRepository.Set(account);
        operationRepository.Set(op);
        events.Publish(new DomainEvent("OperationCreated", op));
        return op;
    }

    public void Cancel(uint id)
    {
        events.Publish(new DomainEvent("OperationCanceled", id));
    }

    public void Remove(uint id)
    {
        Cancel(id);
        operationRepository.Remove(id);
        events.Publish(new DomainEvent("OperationRemoved", id));
    }

    public Operation? Get(uint id) => operationRepository.Get(id);
    public IEnumerable<Operation> GetAll() => operationRepository.GetAll();
}