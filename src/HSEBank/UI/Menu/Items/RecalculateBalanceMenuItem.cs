using HSEBank.Domain.Models;
using HSEBank.Facades;
using HSEBank.Services;

namespace HSEBank.UI.Menu.Items;

public class RecalculateBalanceMenuItem(AccountFacade accFacade, IAccountService accSvc) : IMenuItem
{
    public string Title => "Пересчитать баланс";

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

        try
        {
            accFacade.RecalculateBalance(acc.Id);
            Console.WriteLine("Баланс пересчитан!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Не получилось пересчитать баланс: " + e.Message);
        }
    }
}