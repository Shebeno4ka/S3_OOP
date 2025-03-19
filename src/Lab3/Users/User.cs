using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Users;

public class User : IUser
{
    public Guid Id { get; } = Guid.NewGuid();

    // {message, isRead}
    private readonly Dictionary<Message, bool> _messages = [];

    public bool TrySendMessage(Message message) => _messages.TryAdd(message, false);

    public bool TryMarkRead(Message message) => _messages.TryGetValue(message, out _);
}