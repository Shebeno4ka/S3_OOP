using Itmo.ObjectOrientedProgramming.Lab4.FsStructure;

namespace Itmo.ObjectOrientedProgramming.Lab4.FsVisitors;

public interface IFsComponentVisitor
{
    public int? MaxDepth { get; set; }

    string Visit(FileFsComponent component);

    string Visit(DirectoryFsComponent component);
}