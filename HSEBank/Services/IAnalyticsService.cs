namespace HSEBank.Services;

public interface IAnalyticsService
{
    uint CalculateBalanceDiff(uint accountId, DateTime from, DateTime to);
    IDictionary<string, uint> GroupByCategory(uint accountId, DateTime from, DateTime to);
}