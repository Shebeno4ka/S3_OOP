namespace Itmo.ObjectOrientedProgramming.Lab4.States;

public class LocalConnectionState : IState
{
    public string? Path { get; private set; }

    public bool IsConnected { get; private set; }

    public bool TryConnect(string path)
    {
        if (IsConnected)
            return Path == path;

        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            return false;

        Path = path;
        IsConnected = true;
        return true;
    }

    public void Disconnect()
    {
        Path = null;
        IsConnected = false;
    }
}
