using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors.ExecutionResults;
using Itmo.ObjectOrientedProgramming.Lab4.States;
using Itmo.ObjectOrientedProgramming.Lab4.Utils;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;

public class FileDeleteCommandExecutor : ICommandExecutor
{
    private readonly IState _state;
    private readonly string _path;

    public FileDeleteCommandExecutor(IState state, string path)
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

        if (!File.Exists(absolutePath))
        {
            return new ExecutionResult.ExecutionFailureResult($"File {_path} is not exists.");
        }

        try
        {
            File.Delete(absolutePath);
        }
        catch (Exception ex)
        {
            return new ExecutionResult.ExecutionFailureResult($"Failed to delete file {_path}\n {ex.Message}");
        }

        return new ExecutionResult.ExecutionSuccessResult(string.Empty);
    }
}