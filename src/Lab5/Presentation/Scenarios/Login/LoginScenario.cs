using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Admins;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Login;

public class LoginScenario(AccountLoginScenario accountLoginScenario, AdminLoginScenario adminLoginScenario) : IScenario
{
    private readonly AdminLoginScenario _adminScenario = adminLoginScenario;
    private readonly AccountLoginScenario _accountScenario = accountLoginScenario;

    public string Name => "Login";

    public void Run()
    {
        string mode = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select operation mode:")
                    .AddChoices(["User", "Administrator", "Exit"]));

        if (mode == "Exit")
            return;

        if (mode == "Administrator")
            _adminScenario.Run();

        if (mode == "User")
            _accountScenario.Run();
    }
}