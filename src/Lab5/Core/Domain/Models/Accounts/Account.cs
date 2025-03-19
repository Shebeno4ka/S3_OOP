namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;

public class Account
{
    public Account(AccountNumber number, PinCode code)
    {
        Pin = code;
        AccountNumber = number;
        Balance = Money.Zero();
    }

    public Account(AccountNumber number, PinCode code, Money balance)
    {
        Pin = code;
        AccountNumber = number;
        Balance = balance;
    }

    public PinCode Pin { get; }

    public Money Balance { get; private set; }

    public AccountNumber AccountNumber { get; }

    public void Deposit(Money amount)
    {
        Balance += amount;
    }

    public bool TryWithdraw(Money amount)
    {
        if (Balance < amount)
            return false;

        Balance -= amount;
        return true;
    }
}