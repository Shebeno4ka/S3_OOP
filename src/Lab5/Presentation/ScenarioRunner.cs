using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation;

public class ScenarioRunner(IEnumerable<IScenarioProvider> providers)
{
    private readonly IEnumerable<IScenarioProvider> _providers = providers;

    public void Run()
    {
        IEnumerable<IScenario> scenarios = GetScenarios();

        SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
            .Title("Select action")
            .AddChoices(scenarios)
            .UseConverter(x => x.Name);

        IScenario scenario = AnsiConsole.Prompt(selector);
        scenario.Run();
    }

    private IEnumerable<IScenario> GetScenarios()
    {
        foreach (IScenarioProvider provider in _providers)
        {
            if (provider.TryGetScenario(out IScenario? scenario))
                yield return scenario;
        }
    }
}