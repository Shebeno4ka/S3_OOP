using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors.ExecutionResults;
using Itmo.ObjectOrientedProgramming.Lab4.States;
using Itmo.ObjectOrientedProgramming.Lab4.Utils;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;

public class FileRenameCommandExecutor : ICommandExecutor
{
    private readonly IState _state;
    private readonly string _path;
    private readonly string _name;

    public FileRenameCommandExecutor(IState state, string path, string name)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        _state = state;
        _path = path;
        _name = name;
    }

    public ExecutionResult Execute()
    {
        string? absolutePath = PathsManager.TryTransformToAbsolute(_state, _path);

        if (absolutePath == null)
            return new ExecutionResult.ExecutionFailureResult($"Failed process relative path {_path}");

        if (!File.Exists(absolutePath))
        {
            return new ExecutionResult.ExecutionFailureResult($"File {_path} does not exist.");
        }

        try
        {
            // Получаем директорию файла
            string? directory = Path.GetDirectoryName(absolutePath);

            if (directory == null)
                return new ExecutionResult.ExecutionFailureResult($"Some error occured with renaming {absolutePath}");

            // Формируем новый путь
            string? newFilePath = Path.Combine(directory, _name);

            if (File.Exists(newFilePath))
            {
                return new ExecutionResult.ExecutionFailureResult($"File with name {_name} already exists in the same directory.");
            }

            // Переименовываем файл
            File.Move(absolutePath, newFilePath);
        }
        catch (Exception ex)
        {
            return new ExecutionResult.ExecutionFailureResult($"Failed to rename file {_path} to {_name}\n {ex.Message}");
        }

        return new ExecutionResult.ExecutionSuccessResult($"File {_path} successfully renamed to {_name}");
    }
}
