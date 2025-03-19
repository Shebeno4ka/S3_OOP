using Itmo.ObjectOrientedProgramming.Lab3.Addressees;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Lab3.Tests.Mocks;

internal class MockAddressee : IAddressee
{
    public List<Message> ReceivedMessages { get; } = [];

    public bool TrySendMessage(Message message)
    {
        ReceivedMessages.Add(message);
        return true;
    }
}