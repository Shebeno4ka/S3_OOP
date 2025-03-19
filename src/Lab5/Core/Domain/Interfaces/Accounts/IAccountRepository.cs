using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Interfaces.Accounts;

public interface IAccountRepository
{
    public Task UpdateAccountAsync(Account account);

    public Task<Account?> GetByAccountNumberAsync(AccountNumber accountNumber);

    public Task<Account> CreateAccountAsync(AccountNumber accountNumber, PinCode pin);

    public Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(AccountNumber accountId);
}