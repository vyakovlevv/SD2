using HSEBank.Domain.Events;
using HSEBank.Domain.Factories;
using HSEBank.Domain.Models;
using HSEBank.Repositories;

namespace HSEBank.Services;

public class AccountService(IAccountRepository accountRepository, IDomainFactory domainFactory, IEventBus events) : IAccountService
{
    public BankAccount Create(string name, uint initial)
    {
        var acc = domainFactory.CreateBankAccount(name, initial);
        accountRepository.Set(acc);
        events.Publish(new DomainEvent("AccountCreated", acc));
        return acc;
    }

    public void Remove(uint id)
    {
        accountRepository.Remove(id);
        events.Publish(new DomainEvent("AccountRemoved", id));
    }

    public BankAccount? Get(uint id) => accountRepository.Get(id);
    public IEnumerable<BankAccount> GetAll() => accountRepository.GetAll();
}