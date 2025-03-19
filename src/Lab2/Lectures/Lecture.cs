using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Lectures;

public class Lecture : ILecture
{
    public Guid OriginalId { get; }

    public Guid Id { get; }

    public IUser Author { get; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public string Content { get; private set; }

    public Lecture(IUser author, string name, string description, string content)
        : this(Guid.Empty, author, name, description, content) { }

    public Lecture(ILecture lecture) : this(lecture.Id, lecture.Author, lecture.Name, lecture.Description, lecture.Content) { }

    private Lecture(Guid originalId, IUser author, string name, string description, string content)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Invalid name", nameof(name));
        if (string.IsNullOrEmpty(description))
            throw new ArgumentException("Invalid description", nameof(description));
        if (string.IsNullOrEmpty(content))
            throw new ArgumentException("Invalid content", nameof(content));

        OriginalId = originalId;
        Id = Guid.NewGuid();
        Author = author;
        Name = name;
        Description = description;
        Content = content;
    }

    public ILecture Inherit()
    {
        return new Lecture(Id, Author, Name, Description, Content);
    }

    public bool TryChangeName(IUser user, string newName)
    {
        if (!IsAuthorCorrect(user) || string.IsNullOrEmpty(newName))
            return false;

        Name = newName;
        return true;
    }

    public bool TryChangeDescription(IUser user, string newDescription)
    {
        if (!IsAuthorCorrect(user) || string.IsNullOrEmpty(newDescription))
            return false;

        Description = newDescription;
        return true;
    }

    public bool TryChangeContent(IUser user, string newContent)
    {
        if (!IsAuthorCorrect(user) || string.IsNullOrEmpty(newContent))
            return false;

        Content = newContent;
        return true;
    }

    private bool IsAuthorCorrect(IUser user)
    {
        return user.Id == Author.Id;
    }
}
