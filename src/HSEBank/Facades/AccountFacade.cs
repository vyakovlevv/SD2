using HSEBank.Domain.Events;
using HSEBank.Domain.Factories;
using HSEBank.Domain.Models;
using HSEBank.Repositories;
using HSEBank.Services;

namespace HSEBank.Facades;

public class AccountFacade(IDomainFactory domainFactory, IAccountService accountService, ICategoryService categoryService, IAccountRepository accountRepository, IOperationService operationService, IAnalyticsService analyticsService, IEventBus events)
{
    
    public BankAccount CreateAccountWithInitialDeposit(string name, uint initialDeposit)
    {
        var account = accountService.Create(name, 0);

        var cat = categoryService.GetAll().FirstOrDefault(c => c.Name == "Стартовый депозит")
                  ?? categoryService.Create(OperationType.Income, "Стартовый депозит");

        operationService.Create(OperationType.Income, account.Id, cat.Id, initialDeposit, "Начальный баланс");

        events.Publish(new DomainEvent("AccountCreatedWithDeposit", account));
        Console.WriteLine($"[Facade] Создан счёт '{account.Name}' с начальными {initialDeposit / 100} rub");

        return account;
    }

    public void RecalculateBalance(uint accountId)
    {
        var account = accountService.Get(accountId);
        if (account == null) throw new Exception("Аккаунт не найден");

        var ops = operationService.GetAll().Where(o => o.AccountId == accountId);
        int balance = 0;
        foreach (var op in ops)
        {
            balance += (int)(op.Type == OperationType.Income ? op.Amount : -op.Amount);
        }

        var corrected = domainFactory.CreateBankAccount(account.Name, (uint)balance);
        corrected.Id = account.Id;
        accountService.Remove(account.Id);
        accountRepository.Set(corrected);

        Console.WriteLine($"[Facade] Пересчитан баланс для {account.Name} = {balance}");
    }
}