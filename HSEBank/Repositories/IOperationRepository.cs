using HSEBank.Domain.Models;

namespace HSEBank.Repositories;

public interface IOperationRepository : IRepository<Operation>
{
    public IEnumerable<Operation> GetByAccount(uint id);
}