using HSEBank.Services;

namespace HSEBank.Facades;

public class AnalyticsFacade
{
    private readonly IAnalyticsService _analytics;
    private readonly IAccountService _accounts;

    public AnalyticsFacade(IAnalyticsService analytics, IAccountService accounts)
    {
        _analytics = analytics;
        _accounts = accounts;
    }

    /// <summary>
    /// Сгенерировать краткий отчёт за период.
    /// </summary>
    public void GenerateReport(uint accountId, DateTime from, DateTime to)
    {
        var account = _accounts.Get(accountId);
        if (account == null) throw new Exception("Счёт не найден");

        var diff = _analytics.CalculateBalanceDiff(accountId, from, to);
        var grouped = _analytics.GroupByCategory(accountId, from, to);

        Console.WriteLine($"Отчёт по счёту: {account.Name}");
        Console.WriteLine($"Период: {from:dd.MM.yyyy} - {to:dd.MM.yyyy}");
        Console.WriteLine($"Изменение баланса: {diff / 100} rub");
        Console.WriteLine("Операции по категориям:");
        foreach (var kv in grouped)
        {
            Console.WriteLine($"  - {kv.Key}: {kv.Value / 100} rub");
        }
    }
}