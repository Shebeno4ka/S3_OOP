using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Exceptions;

public class AccountNotFoundException(AccountNumber number) : Exception($"Can't find account by number: {number}");