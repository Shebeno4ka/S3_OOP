namespace Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors.ExecutionResults;

public record ExecutionResult(string ResultString)
{
public record ExecutionSuccessResult(string ResultString) : ExecutionResult(ResultString);

public record ExecutionFailureResult(string ResultString) : ExecutionResult(ResultString);
}