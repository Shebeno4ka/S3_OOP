using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public abstract class CommandHandlerBase : ICommandHandler
{
    private CommandHandlerBase? _next;

    public CommandHandlerBase AddNext(CommandHandlerBase handler)
    {
        if (_next == null)
            _next = handler;
        else
            _next.AddNext(handler);

        return this;
    }

    public ICommandExecutor? Handle(in ICollection<string> tokens)
    {
        ICommandExecutor? parseResult = TryParse(tokens);
        if (parseResult == null)
            return _next?.Handle(tokens);

        return parseResult;
    }

    /// <summary>
    /// Template method, trying to parse command arguments.
    /// </summary>
    /// <param name="tokens">given tokens</param>
    /// <returns>nullable type of ICommandExecutor</returns>
    protected abstract ICommandExecutor? TryParse(in ICollection<string> tokens);
}