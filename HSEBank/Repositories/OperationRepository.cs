using HSEBank.Domain;
using HSEBank.Domain.Models;

namespace HSEBank.Repositories;

public class OperationRepository : Repository<Operation>, IOperationRepository
{
    public IEnumerable<Operation> GetByAccount(uint id)
    {
        return Store.Values.Where(o => o.AccountId == id);
    }
}