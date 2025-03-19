namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Application.Commands;

public abstract record LoginResult
{
    private LoginResult() { }

    public sealed record Success : LoginResult;

    public sealed record NotFound : LoginResult;
}