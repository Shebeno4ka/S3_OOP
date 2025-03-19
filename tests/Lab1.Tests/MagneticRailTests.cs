using Itmo.ObjectOrientedProgramming.Lab1;
using Xunit;

namespace Lab1.Tests;

public class MagneticRailTests
{
    private const double DefaultLength = 500.0;
    private const double DefaultApplyingPower = 1.0;
    private const double TrainMass = 1.0;
    private const double TrainMaxForce = 200000.0;
    private const double TrainAccuracy = 0.1;

    [Fact]
    public void GetSimulationResult_TrainMovesWithApplyingPower_ShouldPass()
    {
        // Arrange
        var rail = new MagneticRail(DefaultLength, DefaultApplyingPower);
        Train train = CreateDefaultTrain();
        train.TryApplyForce(CalculateRequiredForce(10));

        // Act
        SimulationResult result = rail.GetSimulationResult(train);

        // Assert
        Assert.IsType<SimulationResult.Success>(result);
        Assert.True(train.Speed > 0); // Поезд должен двигаться
    }

    [Fact]
    public void GetSimulationResult_TrainWithZeroPower_ShouldNotMove()
    {
        // Arrange
        var rail = new MagneticRail(DefaultLength, 0.0);
        Train train = CreateDefaultTrain();

        // Act
        SimulationResult result = rail.GetSimulationResult(train);

        // Assert
        Assert.IsType<SimulationResult.Failure>(result);
        Assert.Equal(0.0, train.Speed); // Скорость поезда должна быть нулевой
    }

    [Fact]
    public void GetSimulationResult_TrainWithHighSpeed_AndNegativePower_ShouldPass()
    {
        // Arrange
        var rail = new MagneticRail(DefaultLength, -DefaultApplyingPower); // Отрицательная сила
        Train train = CreateDefaultTrain();

        train.TryApplyForce(CalculateRequiredForce(DefaultApplyingPower + 100.0));

        // Act
        SimulationResult result = rail.GetSimulationResult(train);

        // Assert
        Assert.IsType<SimulationResult.Success>(result);
        Assert.True(train.Speed >= 0); // Train didn't start move backward
    }

    private Train CreateDefaultTrain()
    {
        var train = new Train(TrainMass, TrainMaxForce, TrainAccuracy);
        return train;
    }

    // Returns amount of force needed to set speed of the train to target
    private double CalculateRequiredForce(double targetSpeed)
    {
        if (targetSpeed < 0)
        {
            throw new ArgumentException("Target speed cannot be negative.", nameof(targetSpeed));
        }

        return targetSpeed / TrainAccuracy * TrainMass;
    }
}