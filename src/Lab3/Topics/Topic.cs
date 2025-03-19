using Itmo.ObjectOrientedProgramming.Lab3.Addressees;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Topics;

public class Topic : ITopic
{
    private readonly ICollection<IAddressee> _addressees;

    public string Name { get; }

    public Topic(string name, ICollection<IAddressee> addressees)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        ArgumentNullException.ThrowIfNull(addressees);
        if (addressees.Count == 0)
            throw new ArgumentException("At least one addressee is required.");

        Name = name;
        _addressees = addressees;
    }

    public bool TrySendMessage(Message message)
    {
        return _addressees.All(addressee => addressee.TrySendMessage(message));
    }
}