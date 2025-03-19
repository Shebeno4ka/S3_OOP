using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Lab3.Tests.Mocks;
using Xunit;

namespace Lab3.Tests;

public class UserTests
{
    [Fact]
    public void TrySendMessage_AddedMessageNotReadByDefault()
    {
        // Arrange
        var mockUser = new MockUser();
        var message = new Message("Test", "Content", 1);

        // Act
        bool result = mockUser.TrySendMessage(message);

        // Assert

        // Asserting that message is received
        Assert.True(result);
        Assert.Contains(message, mockUser.ReceivedMessages);

        Assert.True(mockUser.MessageStatuses.TryGetValue(message, out bool isRead));

        // Asserting that message is not read
        Assert.False(isRead);
    }

    [Fact]
    public void TryMarkRead_TryMarkReadMessageWhichIsNotReadShouldReturnTrue()
    {
        // Arrange
        var mockUser = new MockUser();
        var message = new Message("Test", "Content", 1);
        mockUser.TrySendMessage(message);

        // Act
        bool result = mockUser.TryMarkRead(message);

        // Assert
        Assert.True(result);
        Assert.True(mockUser.MessageStatuses[message]);
        Assert.Equal(1, mockUser.MarkReadCallCount);
    }

    [Fact]
    public void TryMarkRead_TryMarkReadMessageWhichIsAlreadyReadShouldReturnFalse()
    {
        // Arrange
        var mockUser = new MockUser();
        var message = new Message("Test", "Content", 1);
        mockUser.TrySendMessage(message);
        mockUser.TryMarkRead(message);

        // Act
        bool result = mockUser.TryMarkRead(message);

        // Assert
        Assert.False(result);
        Assert.Equal(2, mockUser.MarkReadCallCount);
    }
}