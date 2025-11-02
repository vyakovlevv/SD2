using HSEBank.Domain.Models;
using HSEBank.Repositories;

namespace HSEBank.Domain.Factories;

public class DomainFactory(
    IAccountRepository accountRepository,
    ICategoryRepository categoryRepository,
    IOperationRepository operationRepository) : IDomainFactory
{
    public BankAccount CreateBankAccount(string name, uint initialBalance = 0)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        uint availableId = accountRepository.GetAvailableId();
        return new BankAccount(availableId, name, initialBalance);
    }

    public Category CreateCategory(OperationType type, string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        uint availableId = categoryRepository.GetAvailableId();
        return new Category(availableId, type, name);
    }

    public Operation CreateOperation(OperationType type, uint bankAccountId, uint categoryId, uint amount, string desc = "")
    {
        var account = accountRepository.Get(bankAccountId) ?? throw new ArgumentException("Аккаунт не найден");
        if (categoryRepository.Get(categoryId) == null) throw new ArgumentException("Категория не найдена");
        if (type == OperationType.Expense)
        {
            if (account.Balance < amount) throw new InvalidOperationException("Баланс должен быть больше операции снятия");
        }
        uint availableId = operationRepository.GetAvailableId();
        DateTime currentTime = DateTime.Now;
        return new Operation(availableId, type, bankAccountId, categoryId, amount, currentTime, desc);
    }
}