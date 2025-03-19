using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public interface IEducationalProgramBuilder
{
    IEducationalProgramBuilder AddSubject(int semester, ISubject subject);

    IEducationalProgramBuilder WithAdministrator(IUser administrator);

    IEducationalProgramBuilder WithName(string name);

    EducationalProgram Build();
}