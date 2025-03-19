using Itmo.ObjectOrientedProgramming.Lab4.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.Utils;

public static class PathsManager
{
    public static string? TryTransformToAbsolute(in IState state, string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return null;

        // Проверяем, является ли путь абсолютным
        if (Path.IsPathRooted(path))
            return Path.GetFullPath(path); // Возвращаем абсолютный путь

        // Если путь относительный, преобразуем его в абсолютный с учётом текущего пути подключения
        string? currentDirectory = state.Path;

        if (string.IsNullOrWhiteSpace(currentDirectory))
            return null;

        // Комбинируем текущий путь и переданный относительный путь
        string absolutePath = Path.Combine(currentDirectory, path);

        // Возвращаем нормализованный абсолютный путь
        return Path.GetFullPath(absolutePath);
    }
}