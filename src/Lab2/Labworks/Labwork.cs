using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public class Labwork : ILabwork
{
    public Guid OriginalId { get; }

    public Guid Id { get; }

    public IUser Author { get; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public int NumOfScores { get; private set; }

    public string EvaluationCriteria { get; private set; }

    public Labwork(IUser author, int numOfScores, string name, string description, string evaluationCriteria)
        : this(Guid.Empty, author, name, description, numOfScores, evaluationCriteria) { }

    private Labwork(Guid originalId, IUser author, string name, string description, int numOfScores, string evaluationCriteria)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrEmpty(description))
            throw new ArgumentException("Description cannot be empty", nameof(description));
        if (string.IsNullOrEmpty(evaluationCriteria))
            throw new ArgumentException("EvaluationCriteria cannot be empty", nameof(evaluationCriteria));

        OriginalId = originalId;

        Id = Guid.NewGuid();
        Author = author;
        Name = name;
        Description = description;
        NumOfScores = numOfScores;
        EvaluationCriteria = evaluationCriteria;
    }

    public ILabwork Inherit()
    {
        return new Labwork(Id, Author, Name, Description, NumOfScores, EvaluationCriteria);
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

    public bool TryChangeEvaluationCriteria(IUser user, string newCriteria)
    {
        if (!IsAuthorCorrect(user) || string.IsNullOrEmpty(newCriteria))
            return false;

        EvaluationCriteria = newCriteria;
        return true;
    }

    public bool TryChangeNumOfScores(IUser user, int newNumOfScores)
    {
        if (!IsAuthorCorrect(user))
            return false;

        NumOfScores = newNumOfScores;
        return true;
    }

    private bool IsAuthorCorrect(IUser user)
    {
        return user.Id == Author.Id;
    }
}