using HSEBank.Domain.Factories;
using HSEBank.Domain.Models;
using HSEBank.Services;

namespace HSEBank.UI.Menu.Items;

public class CreateCategoryMenuItem(ICategoryService categoryService) : IMenuItem
{
    public string Title => "Создать категорию";

    public void Execute()
    {
        Console.Write("Введите название категории: ");
        string name = Console.ReadLine() ?? "Без имени";

        Console.Write("Тип (1 - Доход, 2 - Расход): ");
        var input = Console.ReadLine();
        OperationType type;
        if (input == "1")
        {
            type = OperationType.Income;
        }
        else if (input == "2")
        {
            type = OperationType.Expense;
        }
        else
        {
            Console.WriteLine("Вы ввели некорректный тип операции");
            return;
        }

        try
        {
            categoryService.Create(type, name);
        }
        catch (Exception e)
        {
            Console.WriteLine("Не получилось создать категорию: " + e.Message);
            return;
        }
        Console.WriteLine($"Категория '{name}' создана ({type})");
    }
}