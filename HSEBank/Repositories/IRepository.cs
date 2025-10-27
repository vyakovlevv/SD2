using HSEBank.Domain;
using HSEBank.Domain.Models;

namespace HSEBank.Repositories;

public interface IRepository<T> where T: IUnique
{
    void Set(T obj);
    void Remove(uint id);
    T? Get(uint id);
    IEnumerable<T> GetAll();

    uint GetAvailableId();
}