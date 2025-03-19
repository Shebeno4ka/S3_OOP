using Itmo.ObjectOrientedProgramming.Lab4.FsVisitors;

namespace Itmo.ObjectOrientedProgramming.Lab4.FsStructure;

public class DirectoryFsComponent : IFsComponent
{
    public string Name { get; }

    public IReadOnlyCollection<IFsComponent> Sons { get; }

    public DirectoryFsComponent(string name, IReadOnlyCollection<IFsComponent> sons)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be empty");

        Name = name;
        Sons = sons;
    }

    public string Accept(IFsComponentVisitor visitor)
    {
        return visitor.Visit(this);
    }
}