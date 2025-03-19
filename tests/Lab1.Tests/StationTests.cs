using Itmo.ObjectOrientedProgramming.Lab1;
using Xunit;

namespace Lab1.Tests;

public class StationTests
{
    private const double SpeedLimit = 50.0;
    private const double PassengerHandlingTime = 10.0;
    private const double TrainMass = 10.0;
    private const double TrainMaxForce = 20000.0;
    private const double TrainAccuracy = 0.1;

    [Fact]
    public void GetSimulationResult_TrainSpeedWithinLimit_ShouldPass()
    {
        // Arrange
        var station = new Station(SpeedLimit, PassengerHandlingTime);
        Train train = CreateDefaultTrain();
        train.TryApplyForce(CalculateRequiredForce(SpeedLimit - 10.0));

        // Act
        SimulationResult result = station.GetSimulationResult(train);

        // Assert
        if (result is SimulationResult.Success(var time))
        {
            Assert.Equal(PassengerHandlingTime, time); // Проверка, что мы поезд останавливался и мы потратили время на пассажиров
        }
        else
        {
            Assert.Fail();
        }
    }

    [Fact]
    public void GetSimulationResult_TrainSpeedExceedsLimit_ShouldFail()
    {
        // Arrange
        var station = new Station(SpeedLimit, PassengerHandlingTime);
        Train train = CreateDefaultTrain();
        train.TryApplyForce(CalculateRequiredForce(SpeedLimit + 10.0));

        // Act
        SimulationResult result = station.GetSimulationResult(train);

        // Assert
        Assert.IsType<SimulationResult.Failure>(result);
    }

    [Fact]
    public void GetSimulationResult_TrainSpeedExactlyAtLimit_ShouldPass()
    {
        // Arrange
        var station = new Station(SpeedLimit, PassengerHandlingTime);
        Train train = CreateDefaultTrain();
        train.TryApplyForce(CalculateRequiredForce(SpeedLimit));

        // Act
        SimulationResult result = station.GetSimulationResult(train);

        // Assert
        if (result is SimulationResult.Success(var time))
        {
            Assert.Equal(PassengerHandlingTime, time); // Проверка, что мы потратили время только на загрузку пассажиров
        }
        else
        {
            Assert.Fail();
        }
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