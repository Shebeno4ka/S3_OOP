namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayAddressees;

public interface IDisplayDriver
{
    bool TryClearOutput();

    bool TryWrite(string text);
}