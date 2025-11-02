using HSEBank.Domain;
using HSEBank.Domain.Models;

namespace HSEBank.Repositories;

public abstract class Repository<T> : IRepository<T>  where T: IUnique
{
    protected readonly Dictionary<uint, T> Store = new();

    public void Set(T obj)
    {
        Store[obj.Id] = obj;
    }

    public void Remove(uint id)
    {
        Store.Remove(id);
    }

    public T? Get(uint id)
    {
        return Store.GetValueOrDefault(id);
    }

    public IEnumerable<T> GetAll()
    {
        return Store.Values.ToList();
    }

    public uint GetAvailableId()
    {
        return (uint)Store.Count + 1;
    }
}