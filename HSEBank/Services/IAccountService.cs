using HSEBank.Domain.Models;

namespace HSEBank.Services;

public interface IAccountService
{
    BankAccount Create(string name, uint initial);
    void Remove(uint id);
    BankAccount? Get(uint id);
    IEnumerable<BankAccount> GetAll();
}