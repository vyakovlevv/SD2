using HSEBank.Domain.Models;
using HSEBank.Facades;
using HSEBank.Services;

namespace HSEBank.UI.Menu.Items;

public class AnalyticsMenuItem(AnalyticsFacade analyticsFacade, IAccountService accSvc) : IMenuItem
{
    public string Title => "Аналитика по счёту";

    public void Execute()
    {
        var accounts = accSvc.GetAll().ToList();
        if (!accounts.Any())
        {
            Console.WriteLine("Нет счетов.");
            return;
        }

        Console.WriteLine("Выберите счёт:");
        for (int i = 0; i < accounts.Count; i++)
            Console.WriteLine($"{i + 1}. {accounts[i].Name}");

        BankAccount acc = null;

        try
        {
            acc = accounts[int.Parse(Console.ReadLine() ?? "1") - 1];

        }
        catch (Exception e)
        {
            Console.WriteLine("Некорректный ввод номера счета: " + e.Message);
            return;
        }


        DateTime from, to;
        try
        {
            Console.Write("Начало периода (yyyy-mm-dd): ");
            from = DateTime.Parse(Console.ReadLine() ?? DateTime.UtcNow.AddDays(-30).ToString());

            Console.Write("Конец периода (yyyy-mm-dd): ");
            to = DateTime.Parse(Console.ReadLine() ?? DateTime.UtcNow.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine("Некорректный ввод периода: " + e.Message);
            return;
        }
        

        try
        {
            analyticsFacade.GenerateReport(acc.Id, from, to);
        }
        catch (Exception e)
        {
            Console.WriteLine("Не получилось создать отчет: " + e.Message);
            return;
        }
    }
}