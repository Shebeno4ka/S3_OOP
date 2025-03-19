using Itmo.ObjectOrientedProgramming.Lab4.FsVisitors;

namespace Itmo.ObjectOrientedProgramming.Lab4.FsStructure;

public class FileFsComponent : IFsComponent
{
    public string Name { get; }

    public FileFsComponent(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be empty");
        Name = name;
    }

    public string Accept(IFsComponentVisitor visitor)
    {
        return visitor.Visit(this);
    }
}