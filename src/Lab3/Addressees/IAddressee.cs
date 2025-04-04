using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressees;

public interface IAddressee
{
    bool TrySendMessage(Message message);
}