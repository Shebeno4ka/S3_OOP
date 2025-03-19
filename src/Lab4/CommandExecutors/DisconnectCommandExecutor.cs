using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors.ExecutionResults;
using Itmo.ObjectOrientedProgramming.Lab4.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;

public class DisconnectCommandExecutor(IState state) : ICommandExecutor
{
    private readonly IState _state = state;

    public ExecutionResult Execute()
    {
        if (_state.IsConnected)
            return new ExecutionResult.ExecutionFailureResult($"Not connected yet.");

        _state.Disconnect();
        return new ExecutionResult.ExecutionSuccessResult($"Disconnected.");
    }
}