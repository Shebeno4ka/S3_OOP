namespace Itmo.ObjectOrientedProgramming.Lab1;

/// <summary>
/// Immutable class for containing route data: parts of route and maximum speed at the end.
/// </summary>
/// <param name="routeParts">IEnumerable of route parts.</param>
/// <param name="endMaxSpeed">Max speed that can be of train at the end.</param>
public class Route(IEnumerable<IRoutePart> routeParts, double endMaxSpeed)
{
    /// <summary>
    /// Gets parts of the route.
    /// </summary>
    public IEnumerable<IRoutePart> RouteParts { get; private set; } = routeParts;

    /// <summary>
    /// Gets max speed of the route.
    /// </summary>
    public double EndMaxSpeed { get; } = endMaxSpeed;
}