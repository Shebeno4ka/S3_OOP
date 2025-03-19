using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public class EducationalProgram : IEducationalProgram
{
    public Guid Id { get; }

    public string Name { get; }

    public IUser Administrator { get; }

    public Dictionary<int, List<ISubject>> SubjectsBySemester { get; }

    public EducationalProgram(Dictionary<int, List<ISubject>> subjectsBySemester, IUser administrator, string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        if (subjectsBySemester.Count == 0)
            throw new ArgumentException("Educational program have to contain at least one subject", nameof(subjectsBySemester));

        SubjectsBySemester = subjectsBySemester;
        Administrator = administrator;
        Name = name;
        Id = Guid.NewGuid();
    }
}