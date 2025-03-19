using Itmo.ObjectOrientedProgramming.Lab3.Addressees;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.FilterProxyAddressees;

public class FilterProxyAddressee(int minimumImportanceLevel, IAddressee addressee) : IAddressee
{
    private IAddressee Addressee { get; } = addressee;

    public bool TrySendMessage(Message message)
    {
        if (message.ImportanceLevel < minimumImportanceLevel)
            return false;

        return Addressee.TrySendMessage(message);
    }
}