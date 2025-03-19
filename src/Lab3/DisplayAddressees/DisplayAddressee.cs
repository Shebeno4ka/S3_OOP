using Itmo.ObjectOrientedProgramming.Lab3.Addressees;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayAddressees;

public class DisplayAddressee(IDisplayDriver displayDriver) : IAddressee
{
    private readonly IDisplayDriver _displayDriver = displayDriver;

    public bool TrySendMessage(Message message)
    {
        return _displayDriver.TryClearOutput() && _displayDriver.TryWrite(message.Content);
    }
}