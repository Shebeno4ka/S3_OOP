namespace Itmo.ObjectOrientedProgramming.Lab4.FsStructure;

public class FsComponentFactory
{
    public DirectoryFsComponent CreateFromDirectory(string pathToDirectory)
    {
        if (Directory.Exists(pathToDirectory))
        {
            string? name = Path.GetFileName(pathToDirectory);

            IEnumerable<string> names = Directory
                .EnumerateFileSystemEntries(pathToDirectory)
                .Select(Path.GetFileName)
                .Where(x => x is not null)
                .Cast<string>();

            IFsComponent[] components = names
                .Select(entry => Path.Combine(pathToDirectory, entry))
                .Select(CreateFromPath)
                .ToArray();

            return new DirectoryFsComponent(name ?? string.Empty, components);
        }

        throw new InvalidOperationException($"Fs component at {pathToDirectory} is not a directory or it's not exists.");
    }

    private IFsComponent CreateFromPath(string path)
    {
        if (Directory.Exists(path))
        {
            string? name = Path.GetFileName(path);

            IEnumerable<string> names = Directory
                .EnumerateFileSystemEntries(path)
                .Select(Path.GetFileName)
                .Where(x => x is not null)
                .Cast<string>();

            IFsComponent[] components = names
                .Select(entry => Path.Combine(path, entry))
                .Select(CreateFromDirectory)
                .ToArray();

            return new DirectoryFsComponent(name ?? string.Empty, components);
        }

        if (File.Exists(path))
        {
            string name = Path.GetFileName(path);
            return new FileFsComponent(name);
        }

        throw new InvalidOperationException($"Fs component at {path} is not found");
    }
}