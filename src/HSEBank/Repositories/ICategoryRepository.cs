using HSEBank.Domain.Models;

namespace HSEBank.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    public IEnumerable<Category> GetByType(OperationType type);
}