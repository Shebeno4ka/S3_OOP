using Itmo.ObjectOrientedProgramming.Lab1;
using Xunit;

namespace Lab1.Tests;

public class RailTests
{
    private const double DefaultRailLength = 100.0;
    private const double TrainMass = 1000.0;
    private const double TrainMaxForce = 200.0;
    private const double TrainAccuracy = 1.0;

    [Fact]
    public void GetSimulationResult_TrainCompletesRail_ShouldPass()
    {
        // Arrange
        var rail = new Rail(DefaultRailLength);
        Train train = CreateDefaultTrain();
        train.TryApplyForce(TrainMaxForce); // Применяем силу для начала движения

        // Act
        SimulationResult result = rail.GetSimulationResult(train);

        // Assert
        if (result is SimulationResult.Success(var time))
        {
            Assert.True(time > 0); // Проверка, что время больше 0
        }
        else
        {
            Assert.Fail();
        }
    }

    [Fact]
    public void GetSimulationResult_TrainCannotMove_ShouldFail()
    {
        // Arrange
        var rail = new Rail(DefaultRailLength);
        Train train = CreateDefaultTrain();

        // Act
        SimulationResult result = rail.GetSimulationResult(train);

        // Assert
        Assert.IsType<SimulationResult.Failure>(result);
    }

    private Train CreateDefaultTrain()
    {
        return new Train(TrainMass, TrainMaxForce, TrainAccuracy);
    }
}
