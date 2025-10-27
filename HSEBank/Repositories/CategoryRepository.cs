using HSEBank.Domain;
using HSEBank.Domain.Models;

namespace HSEBank.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public IEnumerable<Category> GetByType(OperationType type)
    {
        return Store.Values.Where(c => c.Type == type);
    }
}