using Itmo.ObjectOrientedProgramming.Lab4.FsStructure;

namespace Itmo.ObjectOrientedProgramming.Lab4.FsVisitors;

public class ConsoleFsComponentVisitor : IFsComponentVisitor
{
    private int _depth;

    public int? MaxDepth { get; set; }

    private readonly string _arrowStr;
    private readonly string _indentStr;

    public ConsoleFsComponentVisitor(string arrowStr = "|–> ", string indentStr = "   ")
    {
        if (string.IsNullOrEmpty(arrowStr))
            throw new ArgumentException("arrowStr cannot be empty");
        if (string.IsNullOrEmpty(indentStr))
            throw new ArgumentException("indentStr cannot be empty");

        _arrowStr = arrowStr;
        _indentStr = indentStr;
    }

    public string Visit(FileFsComponent component)
    {
        return WriteIndented(component.Name);
    }

    public string Visit(DirectoryFsComponent component)
    {
        string res = WriteIndented(component.Name);

        // Если максимум задан и достигнут не идем внутрь
        if (MaxDepth != null && MaxDepth == _depth)
            return res;

        _depth += 1;

        foreach (IFsComponent innerComponent in component.Sons)
        {
            res += innerComponent.Accept(this);
        }

        _depth -= 1;

        return res;
    }

    private string WriteIndented(string value)
    {
        string res = string.Empty;
        if (_depth is not 0)
        {
            res += string.Concat(Enumerable.Repeat(_indentStr, _depth));
            res += _arrowStr;
        }

        res += value;

        return res;
    }
}