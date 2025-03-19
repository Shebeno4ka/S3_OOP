namespace Itmo.ObjectOrientedProgramming.Lab1;

/// <summary>
/// Main simulation class. Have no state.
/// </summary>
public class Simulator
{
    /// <summary>
    /// Simulates movement of the given train on given route.
    /// Simulation status is false if:
    ///     1) Train stopped before reaching the end.
    ///     2) Train did not pass all route parts.
    ///     3) Train speed at the end of route will be above limit.
    /// Stops train after simulation.
    /// </summary>
    /// <returns>Result of the simulation.</returns>
    /// <param name="route">Route to use.</param>
    /// <param name="train">Train to use.</param>
    public SimulationResult GetRouteSimulationResult(Route route, Train train)
    {
        double totalTime = 0;

        foreach (IRoutePart routePart in route.RouteParts)
        {
            SimulationResult currentResult = routePart.GetSimulationResult(train);

            if (currentResult is SimulationResult.Failure)
            {
                return new SimulationResult.Failure();
            }
            else if (currentResult is SimulationResult.Success(var time))
            {
                totalTime += time;
            }
        }

        if (train.Speed > route.EndMaxSpeed)
        {
            return new SimulationResult.Failure();
        }

        train.Halt();

        return new SimulationResult.Success(totalTime);
    }
}