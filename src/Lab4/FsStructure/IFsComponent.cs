using Itmo.ObjectOrientedProgramming.Lab4.FsVisitors;

namespace Itmo.ObjectOrientedProgramming.Lab4.FsStructure;

public interface IFsComponent
{
    public string Name { get; }

    public string Accept(IFsComponentVisitor visitor);
}