namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class Repository<T> : IRepository<T> where T : class, IFindable
{
    private readonly List<T> _items = new List<T>();

    public T? FindById(Guid id)
    {
        return _items.FirstOrDefault(item => item.Id == id);
    }

    public bool Add(T element)
    {
        if (FindById(element.Id) != null)
            return false;

        _items.Insert(0, element);
        return true;
    }
}