using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public interface IEducationalProgram : IFindable
{
    string Name { get; }

    IUser Administrator { get; }

    Dictionary<int, List<ISubject>> SubjectsBySemester { get; }
}