using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<CurrentAccountService>();
        collection.AddScoped<IAccountService, AccountService>();

        return collection;
    }
}