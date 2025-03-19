using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Lectures;

public interface ILecture : IFindable, IInheritable<ILecture>
{
    IUser Author { get; }

    string Name { get; }

    string Description { get; }

    string Content { get; }

    bool TryChangeName(IUser user, string newName);

    bool TryChangeDescription(IUser user, string newDescription);

    bool TryChangeContent(IUser user, string newContent);
}