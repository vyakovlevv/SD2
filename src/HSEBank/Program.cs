using HSEBank.UI.Menu;
using HSEBank.UI.Menu.Items;
using Microsoft.Extensions.DependencyInjection;

namespace HSEBank;

class Program
{
    public static void Main()
    {

        IServiceProvider serviceProvider = DepInjConfig.DepInjConfig.ConfigureServices();
        List<IMenuItem> menuItems =
        [
            serviceProvider.GetRequiredService<AddOperationMenuItem>(),
            serviceProvider.GetRequiredService<AnalyticsMenuItem>(),
            serviceProvider.GetRequiredService<CreateAccountMenuItem>(),
            serviceProvider.GetRequiredService<CreateCategoryMenuItem>(),
            serviceProvider.GetRequiredService<ImportExportMenuItem>(),
            serviceProvider.GetRequiredService<RecalculateBalanceMenuItem>(),
            serviceProvider.GetRequiredService<UndoOperationMenuItem>(),
        ];
        Menu menu = new(menuItems);

        menu.Show();
    }
}