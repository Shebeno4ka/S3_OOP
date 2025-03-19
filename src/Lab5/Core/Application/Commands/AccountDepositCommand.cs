using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Commands;

public sealed record AccountDepositCommand(AccountNumber AccountNumber, PinCode Pin, Money Amount);