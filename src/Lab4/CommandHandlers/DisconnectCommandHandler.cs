using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;
using Itmo.ObjectOrientedProgramming.Lab4.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class DisconnectCommandHandler(IState state) : CommandHandlerBase
{
    private IState State { get; } = state;

    protected sealed override ICommandExecutor? TryParse(in ICollection<string> tokens)
    {
        if (tokens.Count != 1)
            return null;

        return new DisconnectCommandExecutor(State);
    }
}