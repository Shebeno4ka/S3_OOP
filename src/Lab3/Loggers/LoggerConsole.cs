using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Loggers;

public class LoggerConsole : ILogger
{
    public void Log(Message message)
    {
        Console.WriteLine("[Log] " + message.Name + "\nContent: " + message.Content + "\nImportance level: " + message.ImportanceLevel);
    }
}