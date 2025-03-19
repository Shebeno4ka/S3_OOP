using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;
using Itmo.ObjectOrientedProgramming.Lab4.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class FileMoveCommandHandler(IState state) : CommandHandlerBase
{
    protected sealed override ICommandExecutor? TryParse(in ICollection<string> tokens)
    {
        if (tokens.Count != 4)
            return null;

        if (tokens.First() != "file" || tokens.ElementAt(1) != "move")
            return null;

        string sourcePath = tokens.ElementAt(2);
        string destinationPath = tokens.ElementAt(3);

        return new FileMoveCommandExecutor(state, sourcePath, destinationPath);
    }
}
