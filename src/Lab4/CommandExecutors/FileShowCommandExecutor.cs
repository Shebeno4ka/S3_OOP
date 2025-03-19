using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors.ExecutionResults;
using Itmo.ObjectOrientedProgramming.Lab4.FileOutputers;
using Itmo.ObjectOrientedProgramming.Lab4.States;
using Itmo.ObjectOrientedProgramming.Lab4.Utils;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;

public class FileShowCommandExecutor : ICommandExecutor
{
    private readonly IState _state;

    private readonly IFileOutputer _outputer;

    private readonly string _path;

    public FileShowCommandExecutor(IState state, IFileOutputer outputer, string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));

        _state = state;
        _outputer = outputer;
        _path = path;
    }

    public ExecutionResult Execute()
    {
        string? absolutePath = PathsManager.TryTransformToAbsolute(_state, _path);

        if (absolutePath == null)
            return new ExecutionResult.ExecutionFailureResult($"Failed process relative path {_path}");

        try
        {
            _outputer.Output(absolutePath);
        }
        catch (Exception ex)
        {
            return new ExecutionResult.ExecutionFailureResult($"Failed to open or read file {_path}\n {ex.Message}");
        }

        return new ExecutionResult.ExecutionSuccessResult(string.Empty);
    }
}