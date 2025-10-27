using HSEBank.Domain.Models;
using HSEBank.Repositories;

namespace HSEBank.Services;

public class AnalyticsService(IOperationRepository operationRepository, ICategoryRepository categoryRepository)
    : IAnalyticsService
{
    public uint CalculateBalanceDiff(uint accountId, DateTime from, DateTime to)
    {
        var ops = operationRepository.GetByAccount(accountId).Where(o => o.Date >= from && o.Date <= to).ToList();
        var income = ops.Where(o => o.Type == OperationType.Income).Sum(o => o.Amount);
        var expense = ops.Where(o => o.Type == OperationType.Expense).Sum(o => o.Amount);
        return (uint)(income - expense);
    }

    public IDictionary<string, uint> GroupByCategory(uint accountId, DateTime from, DateTime to)
    {
        var ops = operationRepository.GetByAccount(accountId).Where(o => o.Date >= from && o.Date <= to);
        var cats = categoryRepository.GetAll().ToDictionary(c => c.Id, c => c.Name);
        return ops.GroupBy(o => o.CategoryId)
            .ToDictionary(g => cats.GetValueOrDefault(g.Key, "unknown"), g => (uint)g.Sum(o => o.Amount));
    }
}