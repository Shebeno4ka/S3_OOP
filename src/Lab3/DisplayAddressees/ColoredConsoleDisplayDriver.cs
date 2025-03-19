using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayAddressees;

public class ColoredConsoleDisplayDriver(Color color) : IDisplayDriver
{
    private readonly Color _color = color;

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
            Console.Write(Colorize(text), _color);
        }
        catch (IOException)
        {
            return false;
        }

        return true;
    }

    private string Colorize(string text) => Crayon.Output.Rgb(_color.R, _color.G, _color.B).Text(text);
}