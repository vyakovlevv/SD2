using HSEBank.Domain.Factories;
using HSEBank.Domain.Models;
using HSEBank.Repositories;
using HSEBank.Services;

namespace HSEBank.Facades;

public class AccountFacade(IDomainFactory domainFactory, IAccountService accountService, IAccountRepository accountRepository, IOperationService operationService, IAnalyticsService analyticsService)
{

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

        Console.WriteLine($"[Facade] Recalculated balance for {account.Name} = {balance}");
    }
}