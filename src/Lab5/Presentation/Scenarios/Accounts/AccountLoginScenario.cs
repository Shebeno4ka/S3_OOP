using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Commands;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Accounts;

public class AccountLoginScenario(AccountService accountService) : IScenario
{
    private readonly AccountService _accountService = accountService;

    public string Name => "Account Login";

    public async void Run()
    {
        AccountNumber? accountNumber = null;
        PinCode? pin = null;

        try
        {
            string accountNumberStr = AnsiConsole.Prompt(
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

            string pinStr = AnsiConsole.Prompt(
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

            accountNumber = new AccountNumber(accountNumberStr);
            pin = new PinCode(pinStr);

            var query = new AccountGetBalanceCommand(accountNumber, pin);
            await _accountService.HandleAsync(query).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            return;
        }

        await ProceedLoggedAccountMenuAsync(accountNumber, pin).ConfigureAwait(false);
    }

    private async Task ProceedLoggedAccountMenuAsync(AccountNumber accountNumber, PinCode pin)
    {
        while (true)
        {
            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("User Menu:")
                    .AddChoices(
                    [
                        "Check Balance",
                        "Withdraw Money",
                        "Deposit Money",
                        "View Transaction History",
                        "Back to Main Menu",
                    ]));

            if (choice == "Back to Main Menu")
                break;

            try
            {
                switch (choice)
                {
                    case "Check Balance":
                        var query = new AccountGetBalanceCommand(accountNumber, pin);
                        Money balance = await _accountService.HandleAsync(query).ConfigureAwait(false);
                        AnsiConsole.MarkupLine($"[green]Current balance: {balance}[/]");
                        break;

                    case "Withdraw Money":
                        decimal withdrawAmount = AnsiConsole.Prompt(
                            new TextPrompt<decimal>("Enter amount to withdraw:")
                                .Validate(amount =>
                                {
                                    try
                                    {
                                        _ = new Money(amount);
                                        return true;
                                    }
                                    catch (ArgumentException)
                                    {
                                        return false;
                                    }
                                })
                                .ValidationErrorMessage("Please enter a valid positive amount with up to 2 decimal places."));

                        var withdrawCommand = new AccountWithdrawCommand(accountNumber, pin, new Money(withdrawAmount));
                        await _accountService.HandleAsync(withdrawCommand).ConfigureAwait(false);
                        AnsiConsole.MarkupLine("[green]Withdrawal successful.[/]");
                        break;

                    case "Deposit Money":
                        decimal depositAmount = AnsiConsole.Prompt(
                            new TextPrompt<decimal>("Enter amount to deposit:")
                                .Validate(amount =>
                                {
                                    try
                                    {
                                        _ = new Money(amount);
                                        return true;
                                    }
                                    catch (ArgumentException)
                                    {
                                        return false;
                                    }
                                })
                                .ValidationErrorMessage("Please enter a valid positive amount with up to 2 decimal places."));

                        var depositCommand = new AccountDepositCommand(accountNumber, pin, new Money(depositAmount));
                        await _accountService.HandleAsync(depositCommand).ConfigureAwait(false);
                        AnsiConsole.MarkupLine("[green]Deposit successful.[/]");
                        break;

                    case "View Transaction History":
                        IEnumerable<Transaction> transactions = await _accountService.GetTransactionHistoryAsync(accountNumber, pin).ConfigureAwait(false);
                        Table table = new Table()
                            .AddColumn("Type")
                            .AddColumn("Amount")
                            .AddColumn("Date")
                            .AddColumn("Balance");

                        foreach (Transaction transaction in transactions)
                        {
                            table.AddRow(
                                transaction.Type.ToString(),
                                transaction.Amount.ToString(),
                                transaction.TimeStamp.ToString("g"),
                                transaction.Amount.ToString());
                        }

                        AnsiConsole.Write(table);
                        break;
                }
            }
            catch (AccountNotFoundException)
            {
                AnsiConsole.MarkupLine("[red]Account not found.[/]");
                break;
            }
            catch (InsufficientFundsException)
            {
                AnsiConsole.MarkupLine("[red]Insufficient funds.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }
    }
}