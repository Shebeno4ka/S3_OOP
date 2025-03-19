using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors.ExecutionResults;
using Itmo.ObjectOrientedProgramming.Lab4.States;
using Itmo.ObjectOrientedProgramming.Lab4.Utils;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;

public class LocalConnectCommandExecutor : ICommandExecutor
{
    private readonly IState _state;

    private readonly string _path;

    public LocalConnectCommandExecutor(IState state, string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));

        _state = state;
        _path = path;
    }

    public ExecutionResult Execute()
    {
        string? absolutePath = PathsManager.TryTransformToAbsolute(_state, _path);

        if (absolutePath == null)
            return new ExecutionResult.ExecutionFailureResult($"Failed process relative path {_path}");

        return _state.TryConnect(_path)
            ? new ExecutionResult.ExecutionSuccessResult($"Connected successfully to {_path}")
            : new ExecutionResult.ExecutionFailureResult($"Failed to connect to {_path}\nIs path correct?");
    }
}