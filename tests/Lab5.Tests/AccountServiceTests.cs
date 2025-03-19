using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Commands;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;
using NSubstitute;
using Xunit;

namespace Lab5.Tests;

public class AccountServiceTests
{
    private readonly AccountRepositoryMock _repositoryMock = new();
    private readonly CurrentAccountService _currentAccountService = new();
    private readonly AccountService _accountService;

    public AccountServiceTests()
    {
        _accountService = new AccountService(_repositoryMock.Mock, _currentAccountService);
    }

    [Fact]
    public async Task WithdrawMoney_WhenSufficientBalance_ShouldUpdateBalance()
    {
        // Arrange
        var accountNumber = new AccountNumber("123456");
        var pin = new PinCode("12345");
        var initialBalance = new Money(1000);
        var withdrawAmount = new Money(500);

        var account = new Account(accountNumber, pin, initialBalance);
        _repositoryMock.SetupGetByAccountNumberAsync(accountNumber, account);

        // Act
        await _accountService.HandleAsync(new AccountWithdrawCommand(accountNumber, pin, withdrawAmount));

        // Assert
        await _repositoryMock.Mock.Received(1).UpdateAccountAsync(Arg.Is<Account>(acc =>
            acc.AccountNumber == accountNumber &&
            acc.Balance == initialBalance - withdrawAmount));
    }

    [Fact]
    public async Task WithdrawMoney_WhenInsufficientBalance_ShouldThrowException()
    {
        // Arrange
        var accountNumber = new AccountNumber("123456");
        var pin = new PinCode("1234");
        var initialBalance = new Money(100);
        var withdrawAmount = new Money(500);

        var account = new Account(accountNumber, pin, initialBalance);
        _repositoryMock.SetupGetByAccountNumberAsync(accountNumber, account);

        // Act & Assert
        await Assert.ThrowsAsync<InsufficientFundsException>(() =>
            _accountService.HandleAsync(new AccountWithdrawCommand(accountNumber, pin, withdrawAmount)));
    }

    [Fact]
    public async Task DepositMoney_ShouldUpdateBalance()
    {
        // Arrange
        var accountNumber = new AccountNumber("123456");
        var pin = new PinCode("1234");
        var initialBalance = new Money(1000);
        var depositAmount = new Money(500);

        var account = new Account(accountNumber, pin, initialBalance);
        _repositoryMock.SetupGetByAccountNumberAsync(accountNumber, account);

        // Act
        await _accountService.HandleAsync(new AccountDepositCommand(accountNumber, pin, depositAmount));

        // Assert
        await _repositoryMock.Mock.Received(1).UpdateAccountAsync(Arg.Is<Account>(acc =>
            acc.AccountNumber == accountNumber &&
            acc.Balance == initialBalance + depositAmount));
    }
}