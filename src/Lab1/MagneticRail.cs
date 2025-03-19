namespace Itmo.ObjectOrientedProgramming.Lab1;

/// <inheritdoc />
public class MagneticRail(double length, double applyingPower) : Rail(length)
{
    /// <inheritdoc/>
    protected override bool TryProcessIteration(Train train)
    {
        return train.TryApplyForce(applyingPower);
    }

    /// <inheritdoc/>
    protected override bool CanGoForward(Train train)
    {
        return train.Speed > 0 || (train.Speed <= 0 && applyingPower > 0);
    }
}