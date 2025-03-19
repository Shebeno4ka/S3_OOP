using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors.ExecutionResults;
using Itmo.ObjectOrientedProgramming.Lab4.States;
using Itmo.ObjectOrientedProgramming.Lab4.Utils;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;

public class TreeGotoCommandExecutor : ICommandExecutor
{
    private readonly IState _state;

    private readonly string _path;

    public TreeGotoCommandExecutor(IState state, string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));

        _path = path;
        _state = state;
    }

    public ExecutionResult Execute()
    {
        string? absolutePath = PathsManager.TryTransformToAbsolute(_state, _path);

        if (absolutePath == null)
            return new ExecutionResult.ExecutionFailureResult($"Failed process relative path {_path}\n Is it correct?");

        if (File.Exists(absolutePath))
            return new ExecutionResult.ExecutionFailureResult($"\"{absolutePath}\" is a file");

        if (Directory.Exists(absolutePath))
            return new ExecutionResult.ExecutionFailureResult($"Directory \"{absolutePath}\" is not exists.");

        return _state.TryConnect(absolutePath)
            ? new ExecutionResult.ExecutionSuccessResult($"Went successfully to \"{absolutePath}\"")
            : new ExecutionResult.ExecutionFailureResult($"Failed to go to \"{absolutePath}\"\nIs path correct?");
    }
}