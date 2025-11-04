using HSEBank.Facades;

namespace HSEBank.UI.Menu.Items;

public class CreateAccountMenuItem(AccountFacade facade) : IMenuItem
{
    public string Title => "Создать счёт";

    public void Execute()
    {
        Console.Write("Введите имя счёта: ");
        string name = Console.ReadLine() ?? "Без имени";

        Console.Write("Введите начальный депозит: ");
        uint amount = 0;
        try
        {
            amount = (uint)(decimal.Parse(Console.ReadLine() ?? "0") * 100);
        }
        catch (Exception e)
        {
            Console.WriteLine("Возникла ошибка при чтении начального депозита: " + e.Message);
            return;
        }

        try
        {
            facade.CreateAccountWithInitialDeposit(name, amount);

        }
        catch (Exception e)
        {
            Console.WriteLine("Возникла ошибка при создании счета: " + e.Message);
        }
    }
}