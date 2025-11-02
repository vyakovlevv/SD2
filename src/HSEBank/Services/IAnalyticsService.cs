namespace HSEBank.Services;

public interface IAnalyticsService
{
    decimal CalculateBalanceDiff(uint accountId, DateTime from, DateTime to);
    IDictionary<string, decimal> GroupByCategory(uint accountId, DateTime from, DateTime to);
}