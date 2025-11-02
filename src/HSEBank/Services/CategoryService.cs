using HSEBank.Domain.Events;
using HSEBank.Domain.Factories;
using HSEBank.Domain.Models;
using HSEBank.Repositories;

namespace HSEBank.Services;

public class CategoryService(ICategoryRepository categoryRepository, IDomainFactory domainFactory, IEventBus events) : ICategoryService
{
    public Category Create(OperationType type, string name)
    {
        var c = domainFactory.CreateCategory(type, name); 
        categoryRepository.Set(c);
        events.Publish(new DomainEvent("CategoryCreated", c));
        return c;
    }
    public Category? Get(uint id) => categoryRepository.Get(id);
    public IEnumerable<Category> GetAll() => categoryRepository.GetAll();
}