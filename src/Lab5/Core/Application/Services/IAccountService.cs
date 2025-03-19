using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Commands;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Services;

public interface IAccountService
{
    public Task<LoginResult> LoginAsync(AccountNumber number, string pin);

    public Task<Account> HandleAsync(AccountCreateCommand command);

    public Task HandleAsync(AccountWithdrawCommand command);

    public Task HandleAsync(AccountDepositCommand command);

    public Task<Money> HandleAsync(AccountGetBalanceCommand command);

    public Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(AccountNumber accountNumber, PinCode pin);
}