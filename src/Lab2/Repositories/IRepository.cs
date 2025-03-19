namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public interface IRepository<T> where T : IFindable
{
    T? FindById(Guid id);

    bool Add(T element);
}