using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressees;

public class ConsoleAddressee : IAddressee
{
    public bool TrySendMessage(Message message)
    {
        try
        {
            Console.WriteLine(message.Content);
        }
        catch (IOException)
        {
            return false;
        }

        return true;
    }
}