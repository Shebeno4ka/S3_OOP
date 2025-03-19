using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressees;

public class GroupAddressee : IAddressee
{
    private readonly ICollection<IAddressee> _addressees;

    public GroupAddressee(ICollection<IAddressee> addressees)
    {
        ArgumentNullException.ThrowIfNull(addressees);
        if (addressees.Count == 0)
            throw new ArgumentException("At least one addressee is required.");

        _addressees = addressees;
    }

    public bool TrySendMessage(Message message)
    {
        return _addressees.All(addressee => addressee.TrySendMessage(message));
    }
}