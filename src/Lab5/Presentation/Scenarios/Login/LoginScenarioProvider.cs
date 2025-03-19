using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Services;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Login;

public class LoginScenarioProvider(
    AccountService service,
    CurrentAccountService currentUser, string adminPassword) : IScenarioProvider
{
    private readonly AccountService _service = service;
    private readonly CurrentAccountService _currentUser = currentUser;

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.CurrentAccountNumber is not null)
        {
            scenario = null;
            return false;
        }

        var accountLoginScenario = new Accounts.AccountLoginScenario(_service);
        var adminLoginScenario = new Admins.AdminLoginScenario(_service, adminPassword);
        scenario = new LoginScenario(accountLoginScenario, adminLoginScenario);

        return true;
    }
}