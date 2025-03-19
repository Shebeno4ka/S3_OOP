namespace Itmo.ObjectOrientedProgramming.Lab4.FileOutputers;

public class ConsoleFileOutputer : IFileOutputer
{
    /// <summary>
    /// Выводит содержимое файла по указанному абсолютному пути в консоль.
    /// </summary>
    /// <param name="absolutePath">Абсолютный путь к файлу.</param>
    public void Output(string absolutePath)
    {
        string content = File.ReadAllText(absolutePath);
        Console.WriteLine(content);
    }
}