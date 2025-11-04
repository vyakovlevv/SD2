using HSEBank.Domain.Models;
using HSEBank.Facades;
using HSEBank.Services;

namespace HSEBank.UI.Menu.Items;

public class AddOperationMenuItem(OperationFacade opFacade, IAccountService accSvc, ICategoryService catSvc)
    : IMenuItem
{
    public string Title => "Добавить операцию";

    public void Execute()
    {
        var accounts = accSvc.GetAll().ToList();
        var categories = catSvc.GetAll().ToList();

        if (!accounts.Any() || !categories.Any())
        {
            Console.WriteLine("Нет доступных счетов или категорий.");
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

        Console.WriteLine("Выберите категорию:");
        for (int i = 0; i < categories.Count; i++)
            Console.WriteLine($"{i + 1}. {categories[i].Name}");
        
        Category cat = null;
        try
        {
            cat = categories[int.Parse(Console.ReadLine() ?? "1") - 1];
        }
        catch (Exception e)
        {
            Console.WriteLine("Некорректный ввод номера категории: " + e.Message);
            return;
        }

        Console.Write("Введите сумму (без знака): ");
        uint amount = (uint)(decimal.Parse(Console.ReadLine() ?? "0") * 100);

        Console.Write("Описание: ");
        string desc = Console.ReadLine() ?? "";

        try
        {
            opFacade.ExecuteOperation(cat.Type, acc.Id, cat.Id, amount, desc);
        }
        catch (Exception e)
        {
            Console.WriteLine("Не получилось выполнить операцию: " + e.Message);
            return;
        }
    }
}