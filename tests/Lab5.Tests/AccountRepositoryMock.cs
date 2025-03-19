using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Interfaces.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;
using NSubstitute;

namespace Lab5.Tests;

internal class AccountRepositoryMock
{
    public IAccountRepository Mock { get; } = Substitute.For<IAccountRepository>();

    public void SetupGetByAccountNumberAsync(AccountNumber accountNumber, Account? result)
    {
        Mock.GetByAccountNumberAsync(accountNumber).Returns(Task.FromResult<Account?>(result));
    }

    public void SetupCreateAccountAsync(AccountNumber accountNumber, PinCode pin, Account result)
    {
        Mock.CreateAccountAsync(accountNumber, pin).Returns(Task.FromResult(result));
    }

    public void SetupUpdateAccountAsync(Account account)
    {
        Mock.UpdateAccountAsync(account).Returns(Task.CompletedTask);
    }

    public void SetupGetTransactionHistoryAsync(AccountNumber accountNumber, IEnumerable<Transaction> result)
    {
        Mock.GetTransactionHistoryAsync(accountNumber).Returns(Task.FromResult(result));
    }
}