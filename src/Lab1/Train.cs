namespace Itmo.ObjectOrientedProgramming.Lab1;

/// <summary>
/// Class of train. Inherits IForceApplicable.
/// Contains mass and maximum of force, accepted to apply.
/// </summary>
public class Train : IForceApplicable
{
    private readonly double _mass;
    private readonly double _maximumAcceptedForce;

    /// <inheritdoc/>
    public double Accuracy { get; }

    /// <inheritdoc/>
    public double Speed { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Train"/> class.
    /// </summary>
    /// <param name="mass">Mass of train. In grams.</param>
    /// <param name="maximumAcceptedForce">Maximum force which can be applied to train.</param>
    /// <param name="accuracy">Time for update for train speed. In seconds.</param>
    public Train(double mass, double maximumAcceptedForce, double accuracy)
    {
        if (mass <= 0)
        {
            throw new ArgumentException("Mass must be greater than zero.", nameof(mass));
        }

        if (accuracy <= 0)
        {
            throw new ArgumentException("Accuracy must be greater than zero.", nameof(accuracy));
        }

        this._mass = mass;
        this._maximumAcceptedForce = maximumAcceptedForce;
        this.Accuracy = accuracy;
        this.Speed = 0;
    }

    /// <inheritdoc/>
    public bool TryApplyForce(double force)
    {
        if (force > this._maximumAcceptedForce)
        {
            return false;
        }

        double acceleration = force / this._mass;
        double resultedSpeed = this.Speed + (acceleration * this.Accuracy);

        this.Speed = resultedSpeed;
        return true;
    }

    /// <summary>
    /// Stops the train.
    /// </summary>
    public void Halt()
    {
        this.Speed = 0;
    }
}