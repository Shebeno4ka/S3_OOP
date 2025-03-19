using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;
using Itmo.ObjectOrientedProgramming.Lab4.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class FileDeleteCommandHandler(IState state) : CommandHandlerBase
{
    protected sealed override ICommandExecutor? TryParse(in ICollection<string> tokens)
    {
        if (tokens.Count != 3)
            return null;

        if (tokens.First() != "file" || tokens.ElementAt(1) != "delete")
            return null;

        string path = tokens.ElementAt(2);

        return new FileDeleteCommandExecutor(state, path);
    }
}