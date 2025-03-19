namespace Itmo.ObjectOrientedProgramming.Lab2;

public interface IInheritable<T> where T : IInheritable<T>
{
    Guid OriginalId { get; }

    T Inherit();
}