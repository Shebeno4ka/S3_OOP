using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressees;

public class MessengerAddressee : IAddressee
{
    public bool TrySendMessage(Message message)
    {
        try
        {
            Console.WriteLine("[Messenger] " + message.Name + ": " + message.Content);
        }
        catch (IOException)
        {
            return false;
        }

        return true;
    }
}