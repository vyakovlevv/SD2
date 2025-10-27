using HSEBank.Domain.Models;

namespace HSEBank.Services;

public interface ICategoryService
{
    Category Create(OperationType type, string name);
    Category? Get(uint id);
    IEnumerable<Category> GetAll();
}