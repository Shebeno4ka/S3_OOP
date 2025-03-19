using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Admins;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Login;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();
        collection.AddScoped<AdminLoginScenario>();
        collection.AddScoped<AccountLoginScenario>();
        collection.AddScoped<IScenarioProvider, LoginScenarioProvider>();

        return collection;
    }
}