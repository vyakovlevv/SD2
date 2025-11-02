using HSEBank.Domain.Models;

namespace HSEBank.Services;

public interface IOperationService
{
    Operation Create(OperationType type, uint bankAccountId, uint categoryId, uint amount, string desc);
    void Remove(uint id);
    void Cancel(uint id);
    Operation? Get(uint id);
    IEnumerable<Operation> GetAll();
}