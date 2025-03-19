using Itmo.ObjectOrientedProgramming.Lab2.GradingFormats;
using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class Subject : ISubject
{
    public Guid OriginalId { get; }

    public Guid Id { get; }

    public IUser Author { get; }

    public string Name { get; private set; }

    public ICollection<ILabwork> Labs { get; }

    public ICollection<ILecture> Lectures { get; private set; }

    public GradeFormat GradeFormat { get; }

    public Subject(ICollection<ILecture> lectures, ICollection<ILabwork> labs, string name, IUser author, GradeFormat format)
        : this(Guid.Empty, lectures, labs, name, author, format) { }

    private Subject(Guid originalId, ICollection<ILecture> lectures, ICollection<ILabwork> labs, string name, IUser author, GradeFormat gradeFormat)
    {
        // Calculating total score
        int totalScore = labs.Sum(lab => lab.NumOfScores);
        totalScore += gradeFormat.GetAddingPoints();

        if (totalScore != 100)
            throw new ArgumentException("Number of scores must be 100");

        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can't be empty");

        if (lectures.Count == 0 && labs.Count == 0)
            throw new ArgumentException("There must be at least one lecture or lab.");

        OriginalId = originalId;

        Lectures = lectures;
        Labs = labs;
        Name = name;
        Author = author;
        Id = Guid.NewGuid();
        GradeFormat = gradeFormat;
    }

    public ISubject Inherit()
    {
        // Создаем глубокую копию коллекции Lectures
        var clonedLectures = Lectures.Select(lecture => lecture.Inherit()).ToList();

        // Создаем глубокую копию коллекции Labs
        var clonedLabs = Labs.Select(lab => lab.Inherit()).ToList();

        return new Subject(
            Id,
            clonedLectures,
            clonedLabs,
            Name,
            Author,
            GradeFormat);
    }

    public bool TryChangeName(IUser user, string newName)
    {
        if (!IsAuthorCorrect(user) || string.IsNullOrEmpty(newName))
            return false;

        Name = newName;
        return true;
    }

    public bool TryChangeLectures(IUser user, ICollection<ILecture> newLectures)
    {
        if (!IsAuthorCorrect(user))
            return false;

        Lectures = newLectures;
        return true;
    }

    private bool IsAuthorCorrect(IUser user)
    {
        return user.Id == Author.Id;
    }
}
