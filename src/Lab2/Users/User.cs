namespace Itmo.ObjectOrientedProgramming.Lab2.Users;

public class User : IUser
{
    public Guid Id { get; }

    public string Name { get; }

    public User(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        Id = Guid.NewGuid();
        Name = name;
    }
}