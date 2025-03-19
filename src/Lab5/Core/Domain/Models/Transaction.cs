using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;

public sealed record Transaction(DateTime TimeStamp, TransactionTypes Type, AccountNumber AccountNumber, Money Amount);

public enum TransactionTypes
{
    Deposit,
    Withdrawal,
}