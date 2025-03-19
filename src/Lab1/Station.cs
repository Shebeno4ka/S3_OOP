namespace Itmo.ObjectOrientedProgramming.Lab1;

/// <summary>
/// Contains data about station: trains speed limit and time for managing with passengers after trains arrives.
/// Don't stop trains if they speed will above limit.
/// </summary>
public class Station : IRoutePart
{
    private readonly double _speedLimit;
    private readonly double _passengerHandlingTime;

    public Station(double speedLimit, double passengerHandlingTime)
    {
        if (speedLimit < 0)
        {
            throw new ArgumentException("Speed limit must be greater than or equal to zero.", nameof(speedLimit));
        }

        if (passengerHandlingTime < 0)
        {
            throw new ArgumentException("Passenger handling time must be greater than or equal to zero.", nameof(passengerHandlingTime));
        }

        _speedLimit = speedLimit;
        _passengerHandlingTime = passengerHandlingTime;
    }

    /// <inheritdoc/>
    public SimulationResult GetSimulationResult(Train train)
    {
        if (train.Speed <= _speedLimit)
        {
            return new SimulationResult.Success(_passengerHandlingTime);
        }
        else
        {
            return new SimulationResult.Failure();
        }
    }
}
