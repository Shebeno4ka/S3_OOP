using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Commands;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Admins;

public class AdminLoginScenario(AccountService accountService, string adminPassword) : IScenario
{
    private readonly AccountService _accountService = accountService;

    private readonly string _adminPassword = adminPassword;

    public string Name => "AdminLogin";

    public async void Run()
    {
        string password = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter admin password:")
                    .Secret());

        if (password != _adminPassword)
        {
            AnsiConsole.MarkupLine("[red]Invalid password. System will now exit.[/]");
            return;
        }

        await ProceedLoggedAdminMenuAsync().ConfigureAwait(false);
    }

    private async Task ProceedLoggedAdminMenuAsync()
    {
        while (true)
        {
            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Administrator Menu:")
                    .AddChoices(
                    [
                        "Create New Account",
                        "Back",
                    ]));

            if (choice == "Back")
                break;

            if (choice == "Create New Account")
            {
                try
                {
                    string accountNumber = AnsiConsole.Prompt(
                        new TextPrompt<string>("Enter account number:")
                            .Validate(value =>
                            {
                                try
                                {
                                    _ = new AccountNumber(value);
                                    return true;
                                }
                                catch (ArgumentException)
                                {
                                    return false;
                                }
                            })
                            .ValidationErrorMessage("Please enter a valid 6-digit account number."));

                    string pin = AnsiConsole.Prompt(
                        new TextPrompt<string>("Enter PIN:")
                            .Secret()
                            .Validate(value =>
                            {
                                try
                                {
                                    _ = new PinCode(value);
                                    return true;
                                }
                                catch (ArgumentException)
                                {
                                    return false;
                                }
                            })
                            .ValidationErrorMessage("Please enter a valid 4-digit PIN."));

                    var command = new AccountCreateCommand(new AccountNumber(accountNumber), new PinCode(pin));
                    Account account = await _accountService.HandleAsync(command).ConfigureAwait(false);
                    AnsiConsole.MarkupLine("[green]Account created successfully.[/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
                }
            }
        }
    }
}