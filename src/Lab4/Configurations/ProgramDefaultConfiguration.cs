using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;
using Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileOutputers;
using Itmo.ObjectOrientedProgramming.Lab4.FsVisitors;
using Itmo.ObjectOrientedProgramming.Lab4.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.Configurations;

public class ProgramDefaultConfiguration
{
    public string ProceedInput(string[] args)
    {
        var state = new LocalConnectionState();

        CommandHandlerBase handlers = new LocalConnectCommandHandler(state)
            .AddNext(new DisconnectCommandHandler(state))
            .AddNext(new TreeGotoCommandHandler(state))
            .AddNext(new TreeListCommandHandler(state, new ConsoleFsComponentVisitor()))
            .AddNext(new FileShowCommandHandler(state, new FileOutputerFactory()))
            .AddNext(new FileMoveCommandHandler(state))
            .AddNext(new FileCopyCommandHandler(state))
            .AddNext(new FileDeleteCommandHandler(state))
            .AddNext(new FileRenameCommandHandler(state))
            ;

        ICommandExecutor? executor = handlers.Handle(args);

        return executor == null ? "Can't determine command." : executor.Execute().ResultString;
    }
}