using HSEBank.Domain.Models;

namespace HSEBank.Domain.Factories;

public interface IDomainFactory
{
    BankAccount CreateBankAccount(string name, uint initialBalance = 0);
    Category CreateCategory(OperationType type, string name);
    Operation CreateOperation(OperationType type, uint bankAccountId, uint categoryId, uint amount, string desc = "");
}