using Itmo.ObjectOrientedProgramming.Lab1;
using Xunit;

namespace Lab1.Tests;

public class TrainTests
{
    private const double Accuracy = 0.1;
    private const double MaxForce = 100.0;

    [Fact]
    public void Train_Creation_WithPositiveMass_ShouldSucceed()
    {
        // Arrange
        double mass = 1000.0;

        // Act
        var train = new Train(mass, MaxForce, Accuracy);

        // Assert
        Assert.NotNull(train); // Ensure the train object is created successfully
        Assert.Equal(Accuracy, train.Accuracy);
        Assert.Equal(0.0, train.Speed);
    }

    [Fact]
    public void Train_Creation_WithZeroMass_ShouldThrowArgumentException()
    {
        // Arrange
        double mass = 0.0;

        // Act & Assert
        Assert.ThrowsAny<Exception>(() => new Train(mass, MaxForce, Accuracy));
    }

    [Fact]
    public void TryApplyForce_WithValidForce_ShouldIncreaseSpeed()
    {
        // Arrange
        double mass = 1000.0;
        var train = new Train(mass, MaxForce, Accuracy);
        double force = 100.0;

        // Act
        bool result = train.TryApplyForce(force);

        // Assert
        Assert.True(result);
        Assert.True(train.Speed > 0);
    }

    [Fact]
    public void TryApplyForce_WithExcessiveForce_ShouldReturnFalse()
    {
        // Arrange
        double mass = 1000.0;
        var train = new Train(mass, MaxForce, Accuracy);
        double force = 200.0; // greater than maximum accepted force

        // Act
        bool result = train.TryApplyForce(force);

        // Assert
        Assert.False(result);
        Assert.Equal(0.0, train.Speed);
    }

    [Fact]
    public void Halt_ShouldSetSpeedToZero()
    {
        // Arrange
        double mass = 1000.0;
        var train = new Train(mass, MaxForce, Accuracy);
        train.TryApplyForce(100.0); // apply some force

        // Act
        train.Halt();

        // Assert
        Assert.Equal(0.0, train.Speed);
    }
}