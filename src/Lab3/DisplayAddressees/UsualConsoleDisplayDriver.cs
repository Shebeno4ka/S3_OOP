namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayAddressees;

public class UsualConsoleDisplayDriver : IDisplayDriver
{
    public bool TryClearOutput()
    {
        try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            return false;
        }

        return true;
    }

    public bool TryWrite(string text)
    {
        try
        {
            Console.Write(text);
        }
        catch (IOException)
        {
            return false;
        }

        return true;
    }
}