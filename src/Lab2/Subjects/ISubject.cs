using Itmo.ObjectOrientedProgramming.Lab2.GradingFormats;
using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public interface ISubject : IInheritable<ISubject>, IFindable
{
    IUser Author { get; }

    string Name { get; }

    ICollection<ILabwork> Labs { get; }

    ICollection<ILecture> Lectures { get; }

    GradeFormat GradeFormat { get; }

    bool TryChangeName(IUser user, string newName);

    bool TryChangeLectures(IUser user, ICollection<ILecture> newLectures);
}