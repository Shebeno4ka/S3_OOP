namespace Itmo.ObjectOrientedProgramming.Lab1;

/// <summary>
/// Process simulation. Doesn't change train state by default.
/// </summary>
public class Rail : IRoutePart
{
    public double Length { get; private set; }

    public Rail(double length)
    {
        if (length > 0)
        {
            Length = length;
        }
        else
        {
            throw new ArgumentException("Значение не должно быть равно 0.");
        }
    }

    /// <inheritdoc/>
    public SimulationResult GetSimulationResult(Train train)
    {
        double coveredDistance = 0;
        bool passed = true;
        double time = 0;

        while (Length > coveredDistance)
        {
            if (!this.CanGoForward(train))
            {
                passed = false;
                break;
            }

            // If the train rolls out of bounds
            if (coveredDistance < 0.0)
            {
                passed = false;
                break;
            }

            if (!this.TryProcessIteration(train))
            {
                passed = false;
                break;
            }

            coveredDistance += train.Speed * train.Accuracy;

            time += train.Accuracy;
        }

        coveredDistance = 0;

        if (passed)
        {
            return new SimulationResult.Success(time);
        }
        else
        {
            return new SimulationResult.Failure();
        }
    }

    /// <summary>
    /// Does nothing and returns success by default.
    /// Redefine it to customize behaviour of train rail passage.
    /// </summary>
    /// <returns>Result of try.</returns>
    /// <param name="train">Train.</param>
    protected virtual bool TryProcessIteration(Train train)
    {
        return true;
    }

    /// <summary>
    /// Checks is train still can continue to pass the section of rails
    /// </summary>
    /// <returns>Bool, describing ability of train to move forward.</returns>
    /// <param name="train">Train for check.</param>
    protected virtual bool CanGoForward(Train train)
    {
        return train.Speed != 0;
    }
}