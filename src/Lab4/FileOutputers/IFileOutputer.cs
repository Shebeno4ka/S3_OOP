namespace Itmo.ObjectOrientedProgramming.Lab4.FileOutputers;

public interface IFileOutputer
{
    /// <summary>
    /// Выводит содержимое файла по указанному абсолютному пути.
    /// </summary>
    /// <param name="absolutePath">Абсолютный путь к файлу.</param>
    void Output(string absolutePath);
}