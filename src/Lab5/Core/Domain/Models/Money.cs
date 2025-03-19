namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;

public record Money
{
    public decimal Amount { get; init; } // Хранение суммы в рублях с копейками

    public Money(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");
        Amount = amount;
    }

    public static Money Zero() => new Money(0);

    public static Money operator +(Money lhs, Money rhs) => new Money(lhs.Amount + rhs.Amount);

    public static Money operator -(Money lhs, Money rhs)
    {
        if (lhs.Amount < rhs.Amount)
            throw new ArgumentException("Result cannot be negative");
        return new Money(lhs.Amount - rhs.Amount);
    }

    public static bool operator >(Money lhs, Money rhs) => lhs.Amount > rhs.Amount;

    public static bool operator <(Money lhs, Money rhs) => lhs.Amount < rhs.Amount;

    public static bool operator >=(Money lhs, Money rhs) => lhs.Amount >= rhs.Amount;

    public static bool operator <=(Money lhs, Money rhs) => lhs.Amount <= rhs.Amount;

    public override string ToString() => $"{Amount:C}";
}