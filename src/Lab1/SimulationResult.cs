namespace Itmo.ObjectOrientedProgramming.Lab1;

/// <summary>
/// Contains info about simulate try
/// </summary>
public abstract record SimulationResult
{
    public sealed record Success(double Time) : SimulationResult;

    public sealed record Failure : SimulationResult;
}