using HSEBank.Facades;

namespace HSEBank.UI.Menu.Items;

public class UndoOperationMenuItem(OperationFacade facade) : IMenuItem
{
    public string Title => "Отменить последнюю операцию";

    public void Execute()
    {
        try
        {
            facade.UndoLastOperation();
        }
        catch (Exception e)
        {
            Console.WriteLine("Не получилось отменить последнюю операцию: " + e.Message);
            return;
        }
        Console.WriteLine("Последняя операция отменена");
    }
}