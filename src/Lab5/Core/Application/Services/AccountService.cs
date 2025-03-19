using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Commands;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Interfaces.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Services;

public class AccountService(IAccountRepository repository, CurrentAccountService currentAccountService) : IAccountService
{
    private readonly IAccountRepository _repository = repository;

    private CurrentAccountService CurrentAccountService { get; } = currentAccountService;

    public async Task<LoginResult> LoginAsync(AccountNumber number, string pin)
    {
        Account acc = await GetAndValidateAccountAsync(number, new PinCode(pin)).ConfigureAwait(false);

        CurrentAccountService.CurrentAccountNumber = acc;
        return new LoginResult.Success();
    }

    public async Task<Account> HandleAsync(AccountCreateCommand command)
    {
        return await _repository.CreateAccountAsync(command.AccountNumber, command.Pin).ConfigureAwait(false);
    }

    public async Task HandleAsync(AccountWithdrawCommand command)
    {
        Account acc = await GetAndValidateAccountAsync(command.AccountNumber, command.Pin).ConfigureAwait(false);

        if (!acc.TryWithdraw(command.Amount))
        {
            throw new InsufficientFundsException(command.AccountNumber);
        }

        await _repository.UpdateAccountAsync(acc).ConfigureAwait(false);
    }

    public async Task HandleAsync(AccountDepositCommand command)
    {
        Account acc = await GetAndValidateAccountAsync(command.AccountNumber, command.Pin).ConfigureAwait(false);

        acc.Deposit(command.Amount);

        await _repository.UpdateAccountAsync(acc).ConfigureAwait(false);
    }

    public async Task<Money> HandleAsync(AccountGetBalanceCommand command)
    {
        Account acc = await GetAndValidateAccountAsync(command.AccountNumber, command.Pin).ConfigureAwait(false);

        return acc.Balance;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(AccountNumber accountNumber, PinCode pin)
    {
        Account account = await GetAndValidateAccountAsync(accountNumber, pin).ConfigureAwait(false);
        return await _repository.GetTransactionHistoryAsync(accountNumber).ConfigureAwait(false);
    }

    private async Task<Account> GetAndValidateAccountAsync(AccountNumber accountNumber, PinCode pin)
    {
        Account? acc = await _repository.GetByAccountNumberAsync(accountNumber).ConfigureAwait(false);

        if (acc is null || !pin.Verify(acc.Pin))
            throw new AccountNotFoundException(accountNumber);

        return acc;
    }
}