using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;
using Itmo.ObjectOrientedProgramming.Lab4.FileOutputers;
using Itmo.ObjectOrientedProgramming.Lab4.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class FileShowCommandHandler(IState state, IFileOutputerFactory outputerFactory) : CommandHandlerBase
{
    private readonly IFileOutputerFactory _outputerFactory = outputerFactory;

    protected sealed override ICommandExecutor? TryParse(in ICollection<string> tokens)
    {
        if (tokens.Count != 5)
            return null;

        if (tokens.First() != "file" || tokens.ElementAt(1) != "show")
            return null;

        if (tokens.ElementAt(3) != "-m")
            return null;

        string path = tokens.ElementAt(2);
        string mode = tokens.ElementAt(4);

        try
        {
            IFileOutputer outputer = _outputerFactory.Create(mode);
            return new FileShowCommandExecutor(state, outputer, path);
        }
        catch (NotSupportedException)
        {
            return null;
        }
    }
}
