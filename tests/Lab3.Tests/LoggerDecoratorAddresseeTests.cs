using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Lab3.Tests.Mocks;
using Xunit;

namespace Lab3.Tests;

public class LoggerDecoratorAddresseeTests
{
    [Fact]
    public void TrySendMessage_LogsMessageAndDelegatesToAddressee()
    {
        // Arrange
        var mockLogger = new MockLogger();
        var mockAddressee = new MockAddressee();
        var message = new Message("Test", "This is a test message", 1);

        var loggerAdapter = new LoggerDecoratorAddressee<MockAddressee>(mockAddressee, mockLogger);

        // Act
        bool result = loggerAdapter.TrySendMessage(message);

        // Assert
        Assert.True(result);
        Assert.Single(mockLogger.LoggedMessages);
        Assert.Equal(message, mockLogger.LoggedMessages[0]);
        Assert.Contains(message, mockAddressee.ReceivedMessages);
    }
}