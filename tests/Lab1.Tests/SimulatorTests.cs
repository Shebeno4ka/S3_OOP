using Itmo.ObjectOrientedProgramming.Lab1;
using Xunit;

namespace Lab1.Tests;

public class SimulatorTests
{
    private const double TrainMass = 10.0;
    private const double TrainMaxForce = 200000.0;
    private const double TrainAccuracy = 1.0;
    private const double RailLength = 100.0;
    private const double SimulatorMaxSpeed = 100.0;

    private readonly Simulator _simulator = new Simulator();

    [Fact]
    public void Scenario1_ForceRailAcceleratesTrainWithinAllowedSpeed_ShouldPass()
    {
        // Arrange
        Train train = CreateDefaultTrain();
        var routeParts = new List<IRoutePart>
        {
            new MagneticRail(RailLength, CalculateRequiredForce(SimulatorMaxSpeed)),  // Accelerates the train within allowed speed
            new Rail(RailLength),  // Regular rail
        };
        var route = new Route(routeParts, SimulatorMaxSpeed);

        // Act
        SimulationResult result = _simulator.GetRouteSimulationResult(route, train);

        // Assert
        Assert.IsType<SimulationResult.Success>(result);
    }

    [Fact]
    public void Scenario2_ForceRailAcceleratesTrainAboveAllowedSpeed_ShouldFail()
    {
        // Arrange
        Train train = CreateDefaultTrain();
        var routeParts = new List<IRoutePart>
        {
            new MagneticRail(RailLength, CalculateRequiredForce(SimulatorMaxSpeed + 10)),  // Accelerates the train within allowed speed
            new Rail(RailLength),  // Regular rail
        };
        var route = new Route(routeParts, SimulatorMaxSpeed);

        // Act
        SimulationResult result = _simulator.GetRouteSimulationResult(route, train);

        // Assert
        Assert.IsType<SimulationResult.Failure>(result);
    }

    [Fact]
    public void Scenario3_TrainPassesForceRailAndStation_ShouldPass()
    {
        // Arrange
        Train train = CreateDefaultTrain();
        double stationSpeedLimit = 1000.0;
        var routeParts = new List<IRoutePart>
        {
            new MagneticRail(RailLength, CalculateRequiredForce(stationSpeedLimit)),  // Accelerates the train
            new Rail(RailLength),  // Regular rail
            new Station(stationSpeedLimit, 10),  // Train stops at station
            new Rail(RailLength),  // Regular rail
        };
        var route = new Route(routeParts, stationSpeedLimit);

        // Act
        SimulationResult result = _simulator.GetRouteSimulationResult(route, train);

        // Assert
        Assert.IsType<SimulationResult.Success>(result);
    }

    [Fact]
    public void Scenario4_TrainFailsDueToExceedingStationSpeedLimit_ShouldFail()
    {
        // Arrange
        Train train = CreateDefaultTrain();
        double stationSpeedLimit = 100.0;
        var routeParts = new List<IRoutePart>
        {
            new MagneticRail(RailLength, CalculateRequiredForce(stationSpeedLimit + 10)),  // Accelerates the train above station's speed limit
            new Station(stationSpeedLimit, 10),  // Train can't stop
            new Rail(RailLength),  // Regular rail
        };
        var route = new Route(routeParts, stationSpeedLimit - 10);

        // Act
        SimulationResult result = _simulator.GetRouteSimulationResult(route, train);

        // Assert
        Assert.IsType<SimulationResult.Failure>(result);
    }

    [Fact]
    public void Scenario5_TrainFailsDueToExceedingTrackSpeedLimitButNotStation_ShouldFail()
    {
        // Arrange
        Train train = CreateDefaultTrain();
        double stationSpeedLimit = 100.0;
        var routeParts = new List<IRoutePart>
        {
            new MagneticRail(RailLength, CalculateRequiredForce(stationSpeedLimit + 10)),  // Accelerates the train above station's speed limit
            new Rail(RailLength),  // Regular rail
            new Station(stationSpeedLimit, 10),  // Train can't stop
            new Rail(RailLength),  // Regular rail
        };
        var route = new Route(routeParts, stationSpeedLimit + 10);

        // Act
        SimulationResult result = _simulator.GetRouteSimulationResult(route, train);

        // Assert
        Assert.IsType<SimulationResult.Failure>(result);
    }

    [Fact]
    public void Scenario6_TrainSuccessfullyDeceleratesAndPassesStation_ShouldPass()
    {
        // Arrange
        Train train = CreateDefaultTrain();
        double stationSpeedLimit = 100.0;
        var routeParts = new List<IRoutePart>
        {
            new MagneticRail(RailLength, CalculateRequiredForce(stationSpeedLimit + 100)),  // Accelerates above station's speed limit
            new Rail(RailLength),  // Regular rail
            new MagneticRail(RailLength, -CalculateRequiredForce(stationSpeedLimit)),  // Decelerates below station's speed limit
            new Station(stationSpeedLimit, 10),  // Train stops at station
            new Rail(RailLength),  // Regular rail
            new MagneticRail(RailLength, CalculateRequiredForce(stationSpeedLimit + 100)),  // Accelerates above rail speed limit
            new Rail(RailLength),  // Regular rail
            new MagneticRail(RailLength, -CalculateRequiredForce(stationSpeedLimit)),  // Decelerates below rail speed limit
        };
        var route = new Route(routeParts, stationSpeedLimit * stationSpeedLimit);

        // Act
        SimulationResult result = _simulator.GetRouteSimulationResult(route, train);

        // Assert
        Assert.IsType<SimulationResult.Success>(result);
    }

    [Fact]
    public void Scenario7_TrainWithZeroSpeedOnRegularRailOnly_ShouldFail()
    {
        // Arrange
        Train train = CreateDefaultTrain();
        var routeParts = new List<IRoutePart>
        {
            new Rail(RailLength),  // Only a regular rail
        };
        var route = new Route(routeParts, 0);

        // Act
        SimulationResult result = _simulator.GetRouteSimulationResult(route, train);

        // Assert
        Assert.IsType<SimulationResult.Failure>(result);
    }

    [Fact]
    public void Scenario8_TrainFailsDueToOpposingForces_ShouldFail()
    {
        // Arrange
        Train train = CreateDefaultTrain();
        double x = 100.0; // magnetic rail lengths
        double y = 200.0; // force
        var routeParts = new List<IRoutePart>
        {
            new MagneticRail(x, y),  // Applies force in one direction
            new MagneticRail(x, -2 * y),  // Applies force in the opposite direction
        };
        var route = new Route(routeParts, 10000.0);

        // Act
        SimulationResult result = _simulator.GetRouteSimulationResult(route, train);

        // Assert
        Assert.IsType<SimulationResult.Failure>(result);
    }

    private Train CreateDefaultTrain()
    {
        return new Train(TrainMass, TrainMaxForce, TrainAccuracy);
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
