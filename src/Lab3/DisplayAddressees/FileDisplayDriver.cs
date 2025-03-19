namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayAddressees;

public class FileDisplayDriver : IDisplayDriver
{
    private readonly string _filePath;

    public FileDisplayDriver(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or whitespace.", nameof(filePath));

        _filePath = filePath;
    }

    public bool TryClearOutput()
    {
        try
        {
            // Очистка файла, записываем пустую строку
            File.WriteAllText(_filePath, string.Empty);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error clearing output: {ex.Message}");
            return false;
        }
    }

    public bool TryWrite(string text)
    {
        try
        {
            // Добавляем текст в конец файла
            File.AppendAllText(_filePath, text + Environment.NewLine);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
            return false;
        }
    }
}