using HSEBank.Commands;
using HSEBank.Domain.Events;
using HSEBank.Domain.Factories;
using HSEBank.Domain.Handlers;
using HSEBank.Facades;
using HSEBank.IO;
using HSEBank.IO.Factories;
using HSEBank.Repositories;
using HSEBank.Services;
using HSEBank.UI.Menu.Items;
using Microsoft.Extensions.DependencyInjection;

namespace HSEBank.DepInjConfig;

public class DepInjConfig
{
    public static IServiceProvider ConfigureServices()
    {
        ServiceCollection services = new ServiceCollection();
        

        services.AddSingleton<IAccountRepository, AccountRepository>();
        services.AddSingleton<ICategoryRepository, CategoryRepository>();
        services.AddSingleton<IOperationRepository, OperationRepository>();

        services.AddSingleton<IEventBus, EventBus>();

        services.AddSingleton<OperationHandler>(sp =>
        {
            var v = new ValidationHandler();
            var s = new SecuritySystemHandler();
            var l = new LoggingHandler();
            v.SetNext(s);
            s.SetNext(l);
            return v;
        });

        services.AddSingleton<IDomainFactory, DomainFactory>();
        
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<ICategoryService, CategoryService>();
        services.AddSingleton<IOperationService, OperationService>();
        services.AddSingleton<IAnalyticsService, AnalyticsService>();
        
        services.AddTransient<CsvExporter>();
        services.AddTransient<JsonExporter>();
        services.AddTransient<YamlExporter>();
        services.AddSingleton<IDataExporterFactory, DataExporterFactory>();
        
        services.AddTransient<CsvImporter>();
        services.AddTransient<JsonImporter>();
        services.AddTransient<YamlImporter>();

        services.AddSingleton<CommandManager>();
        
        services.AddSingleton<AccountFacade>();
        services.AddSingleton<OperationFacade>();
        services.AddSingleton<AnalyticsFacade>();
        services.AddSingleton<ExportFacade>();
        services.AddTransient<ImportFacade>();

        services.AddTransient<AddOperationMenuItem>();
        services.AddTransient<AnalyticsMenuItem>();
        services.AddTransient<CreateAccountMenuItem>();
        services.AddTransient<CreateCategoryMenuItem>();
        services.AddTransient<ImportExportMenuItem>();
        services.AddTransient<RecalculateBalanceMenuItem>();
        services.AddTransient<UndoOperationMenuItem>();

        
        return services.BuildServiceProvider();
    }
}