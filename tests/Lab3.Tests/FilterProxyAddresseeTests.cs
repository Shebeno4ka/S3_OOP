using Itmo.ObjectOrientedProgramming.Lab3.FilterProxyAddressees;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Lab3.Tests.Mocks;
using Xunit;

namespace Lab3.Tests;

public class FilterProxyAddresseeTests
{
    [Fact]
    public void Message_BelowImportanceThreshold_ShouldNotReachAddressee()
    {
        // Arrange
        const int minimumImportanceLevel = 5;
        var mockAddressee = new MockAddressee();
        var proxy = new FilterProxyAddressee(minimumImportanceLevel, mockAddressee);
        var message = new Message("fe", "fe", minimumImportanceLevel - 1);

        // Act
        bool result = proxy.TrySendMessage(message);

        // Assert
        Assert.False(result);
        Assert.Empty(mockAddressee.ReceivedMessages);
    }

    [Fact]
    public void Message_WithLowImportance_ShouldReachOnlyUserWithoutFilter()
    {
        // Arrange
        const int minimumImportanceLevelForFilter = 5;

        var mockUser = new MockUser();

        var proxyUser1 = new FilterProxyAddressee(minimumImportanceLevelForFilter, mockUser);
        var proxyUser2 = new FilterProxyAddressee(0, mockUser); // No filtering

        var message = new Message("fe", "fe", minimumImportanceLevelForFilter - 1);

        // Act
        bool result1 = proxyUser1.TrySendMessage(message);
        bool result2 = proxyUser2.TrySendMessage(message);

        // Assert
        Assert.False(result1); // User1's filter should block the message
        Assert.True(result2);  // User2 should receive the message
        Assert.Single(mockUser.ReceivedMessages); // mockUse received only one message
    }
}