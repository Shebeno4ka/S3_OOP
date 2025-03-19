using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors.ExecutionResults;
using Itmo.ObjectOrientedProgramming.Lab4.FsStructure;
using Itmo.ObjectOrientedProgramming.Lab4.FsVisitors;
using Itmo.ObjectOrientedProgramming.Lab4.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;

public class TreeListCommandExecutor(IState state, IFsComponentVisitor visitor, int depth = 1) : ICommandExecutor
{
    private readonly IState _state = state;

    private readonly int _depth = depth;

    private readonly IFsComponentVisitor _visitor = visitor;

    public ExecutionResult Execute()
    {
        if (_depth < 1)
            return new ExecutionResult.ExecutionFailureResult("Depth must me natural number.");

        if (_state.Path == null)
            return new ExecutionResult.ExecutionFailureResult("Not connected to file system yet.");

        // Building fs tree
        var fsComponentFactory = new FsComponentFactory();
        DirectoryFsComponent fsTree;

        try
        {
            fsTree = fsComponentFactory.CreateFromDirectory(_state.Path);
        }
        catch (InvalidOperationException ex)
        {
            return new ExecutionResult.ExecutionFailureResult($"Failed to read file system structure from \"{_state.Path}\"\n{ex.Message}");
        }

        // Configuring visitor
        _visitor.MaxDepth = _depth;

        // Running visitor
        string fsTreeView = _visitor.Visit(fsTree);

        return new ExecutionResult.ExecutionSuccessResult(fsTreeView);
    }
}