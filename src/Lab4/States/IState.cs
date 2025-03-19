namespace Itmo.ObjectOrientedProgramming.Lab4.States;

public interface IState
{
    public string? Path { get; }

    public bool IsConnected { get; }

    bool TryConnect(string path);

    void Disconnect();
}