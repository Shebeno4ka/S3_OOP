using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors.ExecutionResults;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;

public interface ICommandExecutor
{
    ExecutionResult Execute();
}