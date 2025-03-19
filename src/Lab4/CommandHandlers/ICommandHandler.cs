    using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;

    namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

    public interface ICommandHandler
    {
        ICommandExecutor? Handle(in ICollection<string> tokens);
    }