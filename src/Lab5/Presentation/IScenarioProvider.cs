using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation;

public interface IScenarioProvider
{
    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}