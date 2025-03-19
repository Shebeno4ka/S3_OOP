using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;
using Itmo.ObjectOrientedProgramming.Lab4.FsVisitors;
using Itmo.ObjectOrientedProgramming.Lab4.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class TreeListCommandHandler(IState state, IFsComponentVisitor visitor) : CommandHandlerBase
{
    protected sealed override ICommandExecutor? TryParse(in ICollection<string> tokens)
    {
        if (tokens.Count < 2)
            return null;

        if (tokens.First() != "tree" || tokens.ElementAt(1) != "list")
            return null;

        int? depth = null;

        if (tokens.Count == 4)
        {
                if (tokens.ElementAt(2) == "-d")
                {
                    if (int.TryParse(tokens.ElementAt(3), out int parsedDepth))
                        depth = parsedDepth;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
        }

        if (depth == null)
        {
            return new TreeListCommandExecutor(state, visitor);
        }

        return new TreeListCommandExecutor(state, visitor, depth.Value);
    }
}
