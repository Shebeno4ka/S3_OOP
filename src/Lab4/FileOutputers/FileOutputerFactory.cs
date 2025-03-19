namespace Itmo.ObjectOrientedProgramming.Lab4.FileOutputers;

public class FileOutputerFactory : IFileOutputerFactory
{
    private readonly Dictionary<string, Func<IFileOutputer>> _outputerCreators;

    public FileOutputerFactory()
    {
        _outputerCreators = new Dictionary<string, Func<IFileOutputer>>
        {
            { "console", () => new ConsoleFileOutputer() },
        };
    }

    public void Register(string mode, Func<IFileOutputer> creator)
    {
        if (string.IsNullOrWhiteSpace(mode))
            throw new ArgumentNullException(nameof(mode));

        ArgumentNullException.ThrowIfNull(creator);

        _outputerCreators[mode] = creator;
    }

    public IFileOutputer Create(string mode)
    {
        if (_outputerCreators.TryGetValue(mode, out Func<IFileOutputer>? creator))
            return creator();

        throw new NotSupportedException($"Mode '{mode}' is not supported.");
    }
}