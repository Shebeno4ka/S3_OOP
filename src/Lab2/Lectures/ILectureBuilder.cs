using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Lectures;

public interface ILectureBuilder
{
    ILectureBuilder WithAuthor(IUser author);

    ILectureBuilder WithName(string name);

    ILectureBuilder WithDescription(string description);

    ILectureBuilder WithContent(string content);

    Lecture Build();
}