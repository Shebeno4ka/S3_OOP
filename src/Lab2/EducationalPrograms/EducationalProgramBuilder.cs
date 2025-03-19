using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public class EducationalProgramBuilder
{
    private readonly Dictionary<int, List<ISubject>> _subjectsBySemester = new();
    private IUser? _administrator;
    private string? _name;

    public EducationalProgramBuilder AddSubject(int semester, ISubject subjectsBySemester)
    {
        _subjectsBySemester[semester].Add(subjectsBySemester);
        return this;
    }

    public EducationalProgramBuilder WithAdministrator(IUser administrator)
    {
        _administrator = administrator;
        return this;
    }

    public EducationalProgramBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public EducationalProgram Build()
    {
        if (_subjectsBySemester.Count == 0)
        {
            throw new InvalidOperationException("At least 1 subject required");
        }

        return new EducationalProgram(
            _subjectsBySemester,
            _administrator ?? throw new ArgumentNullException(),
            _name ?? throw new ArgumentNullException());
    }
}
