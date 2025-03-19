using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Lab3.Tests.Mocks;

internal class MockLogger : ILogger
{
    public List<Message> LoggedMessages { get; } = [];

    public void Log(Message message)
    {
        LoggedMessages.Add(message);
    }
}