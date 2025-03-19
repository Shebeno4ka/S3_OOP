using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Users;
using System.Collections.ObjectModel;

namespace Lab3.Tests.Mocks;

internal class MockUser : IUser
{
    public Guid Id { get; } = Guid.NewGuid();

    public Collection<Message> ReceivedMessages { get; } = [];

    public Dictionary<Message, bool> MessageStatuses { get; } = [];

    public int SendMessageCallCount { get; private set; } = 0;

    public int MarkReadCallCount { get; private set; } = 0;

    public bool TrySendMessage(Message message)
    {
        SendMessageCallCount++;
        if (MessageStatuses.ContainsKey(message))
            return false;

        MessageStatuses[message] = false;
        ReceivedMessages.Add(message);
        return true;
    }

    public bool TryMarkRead(Message message)
    {
        MarkReadCallCount++;
        if (!MessageStatuses.TryGetValue(message, out bool isRead) || isRead)
            return false;
        MessageStatuses[message] = true;
        return true;
    }
}