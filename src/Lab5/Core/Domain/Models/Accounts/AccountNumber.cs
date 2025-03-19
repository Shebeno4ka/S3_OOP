namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;

public sealed record AccountNumber
{
    public string Value { get; }

    public AccountNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Account number cannot be empty");

        if (value.Length != 6)
            throw new ArgumentException("Account number must be 6 digits");

        if (!value.All(char.IsDigit))
            throw new ArgumentException("Account number must contain only digits");

        Value = value;
    }

    public static implicit operator string(AccountNumber number) => number.Value;
}
