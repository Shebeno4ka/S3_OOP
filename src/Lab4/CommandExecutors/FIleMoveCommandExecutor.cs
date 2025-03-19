using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors.ExecutionResults;
using Itmo.ObjectOrientedProgramming.Lab4.States;
using Itmo.ObjectOrientedProgramming.Lab4.Utils;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;

public class FileMoveCommandExecutor : ICommandExecutor
{
    private readonly IState _state;

    private readonly string _sourcePath;

    private readonly string _destinationPath;

    public FileMoveCommandExecutor(IState state, string sourcePath, string destinationPath)
    {
        if (string.IsNullOrWhiteSpace(sourcePath))
            throw new ArgumentNullException(nameof(sourcePath));

        if (string.IsNullOrWhiteSpace(destinationPath))
            throw new ArgumentNullException(nameof(destinationPath));

        _state = state;
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public ExecutionResult Execute()
    {
        string? absoluteSourcePath = PathsManager.TryTransformToAbsolute(_state, _sourcePath);
        string? absoluteDestinationPath = PathsManager.TryTransformToAbsolute(_state, _destinationPath);

        if (absoluteSourcePath == null)
            return new ExecutionResult.ExecutionFailureResult($"Failed to process relative source path {_sourcePath}");

        if (absoluteDestinationPath == null)
            return new ExecutionResult.ExecutionFailureResult($"Failed to process relative destination path {_destinationPath}");

        if (!File.Exists(absoluteSourcePath))
            return new ExecutionResult.ExecutionFailureResult($"Source file does not exist: {absoluteSourcePath}");

        if (!Directory.Exists(absoluteDestinationPath))
            return new ExecutionResult.ExecutionFailureResult($"Destination directory does not exist: {absoluteDestinationPath}");

        string destinationFilePath = Path.Combine(absoluteDestinationPath, Path.GetFileName(absoluteSourcePath));

        try
        {
            File.Move(absoluteSourcePath, destinationFilePath);
        }
        catch (Exception ex)
        {
            return new ExecutionResult.ExecutionFailureResult($"Failed to move file from {_sourcePath} to {_destinationPath}\n {ex.Message}");
        }

        return new ExecutionResult.ExecutionSuccessResult($"Moved from {_sourcePath} to {_destinationPath}.");
    }
}
