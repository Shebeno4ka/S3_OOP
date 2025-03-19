namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public class Message : IEquatable<Message>
{
    private readonly Guid _id;

    public string Name { get; }

    public string Content { get; }

    public int ImportanceLevel { get; }

    public Message(string name, string content, int importanceLevel)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name), "name cannot be null or empty.");
        if (string.IsNullOrEmpty(content))
            throw new ArgumentNullException(nameof(content), "content cannot be null or empty.");

        _id = Guid.NewGuid();
        Name = name;
        Content = content;
        ImportanceLevel = importanceLevel;
    }

    public bool Equals(Message? other)
    {
        return other is not null && _id == other._id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Message);
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
}