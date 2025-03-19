using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Services;

public class CurrentAccountService
{
    public Account? CurrentAccountNumber { get; set; }

    public bool IsAdmin { get; set; } = false;
}