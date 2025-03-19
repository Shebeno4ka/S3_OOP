using Itmo.ObjectOrientedProgramming.Lab2.GradingFormats;
using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public interface ISubjectBuilder
{
    ISubjectBuilder WithName(string name);

    ISubjectBuilder WithAuthor(IUser author);

    ISubjectBuilder WithGradingFormat(GradeFormat gradeFormat);

    ISubjectBuilder AddLab(ILabwork lab);

    ISubjectBuilder AddLecture(ILecture lecture);

    Subject Build();
}