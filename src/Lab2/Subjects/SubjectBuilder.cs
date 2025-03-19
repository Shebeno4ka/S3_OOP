using Itmo.ObjectOrientedProgramming.Lab2.GradingFormats;
using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class SubjectBuilder : ISubjectBuilder
{
    private readonly List<ILabwork> _labs = [];
    private readonly List<ILecture> _lectures = [];
    private string? _name;
    private IUser? _author;
    private GradeFormat? _gradeFormat;

    public ISubjectBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ISubjectBuilder WithAuthor(IUser author)
    {
        _author = author;
        return this;
    }

    public ISubjectBuilder WithGradingFormat(GradeFormat gradeFormat)
    {
        _gradeFormat = gradeFormat;
        return this;
    }

    public ISubjectBuilder AddLab(ILabwork lab)
    {
        _labs.Add(lab);
        return this;
    }

    public ISubjectBuilder AddLecture(ILecture lecture)
    {
        _lectures.Add(lecture);
        return this;
    }

    public Subject Build()
    {
        return new Subject(
            _lectures,
            _labs,
            _name ?? throw new ArgumentNullException(),
            _author ?? throw new ArgumentNullException(),
            _gradeFormat ?? throw new ArgumentNullException());
    }
}