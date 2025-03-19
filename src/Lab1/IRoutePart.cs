namespace Itmo.ObjectOrientedProgramming.Lab1;

/// <summary>
/// Interface of route parts.
/// </summary>
public interface IRoutePart
{
    /// <summary>
    /// Simulates train route part passage.
    /// </summary>
    /// <param name="train">Train to passage.</param>
    /// <returns>Result of simulation.</returns>
    SimulationResult GetSimulationResult(Train train);
}