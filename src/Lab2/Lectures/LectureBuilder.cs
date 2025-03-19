using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Lectures;

public class LectureBuilder : ILectureBuilder
{
    private IUser? _author;
    private string? _name;
    private string? _description;
    private string? _content;

    public ILectureBuilder WithAuthor(IUser author)
    {
        _author = author;
        return this;
    }

    public ILectureBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ILectureBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public ILectureBuilder WithContent(string content)
    {
        _content = content;
        return this;
    }

    public Lecture Build()
    {
        if (string.IsNullOrEmpty(_name))
            throw new InvalidOperationException("Name must be set.");
        if (string.IsNullOrEmpty(_description))
            throw new InvalidOperationException("Description must be set.");
        if (string.IsNullOrEmpty(_content))
            throw new InvalidOperationException("Content must be set.");

        return new Lecture(
            _author ?? throw new ArgumentNullException(),
            _name,
            _description,
            _content);
    }
}