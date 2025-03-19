using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;
using Itmo.ObjectOrientedProgramming.Lab4.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class LocalConnectCommandHandler(IState state) : CommandHandlerBase
{
    private IState State { get; } = state;

    protected sealed override ICommandExecutor? TryParse(in ICollection<string> tokens)
    {
        if (tokens.First() != "connect")
            return null;

        if (tokens.Count != 4)
            return null;

        string path = tokens.ElementAt(1);
        string mode = tokens.ElementAt(3);

        if (mode != "local")
            return null;

        return new LocalConnectCommandExecutor(State, path);
    }
}