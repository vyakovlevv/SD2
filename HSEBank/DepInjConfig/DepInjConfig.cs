using HSEBank.Commands;
using HSEBank.Domain.Events;
using HSEBank.Domain.Handlers;
using HSEBank.Repositories;
using HSEBank.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HSEBank.DepInjConfig;

public class DepInjConfig
{
    public IServiceProvider ConfigureServices()
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

        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<ICategoryService, CategoryService>();
        services.AddSingleton<IOperationService, OperationService>();
        services.AddSingleton<IAnalyticsService, AnalyticsService>();

        // Facades
        services.AddSingleton<AccountFacade>();
        services.AddSingleton<CategoryFacade>();
        services.AddSingleton<OperationFacade>();
        services.AddSingleton<AnalyticsFacade>();

        // Command manager (transient in this demo)
        services.AddSingleton<CommandManager>();
        
        return services.BuildServiceProvider();
    }
}