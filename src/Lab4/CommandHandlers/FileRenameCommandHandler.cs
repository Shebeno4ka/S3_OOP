using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;
using Itmo.ObjectOrientedProgramming.Lab4.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class FileRenameCommandHandler(IState state) : CommandHandlerBase
{
    protected sealed override ICommandExecutor? TryParse(in ICollection<string> tokens)
    {
        if (tokens.Count != 4)
            return null;

        if (tokens.First() != "file" || tokens.ElementAt(1) != "rename")
            return null;

        string path = tokens.ElementAt(2);
        string name = tokens.ElementAt(3);

        return new FileRenameCommandExecutor(state, path, name);
    }
}
