using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public class LabworkBuilder : ILabworkBuilder
{
    private IUser? _author;
    private string? _name;
    private string? _description;
    private int? _numOfScores;
    private string? _evaluationCriteria;

    public ILabworkBuilder WithAuthor(IUser author)
    {
        _author = author;
        return this;
    }

    public ILabworkBuilder WithName(string name)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
        return this;
    }

    public ILabworkBuilder WithDescription(string description)
    {
        _description = description ?? throw new ArgumentNullException(nameof(description));
        return this;
    }

    public ILabworkBuilder WithNumOfScores(int numOfScores)
    {
        _numOfScores = numOfScores;
        return this;
    }

    public ILabworkBuilder WithEvaluationCriteria(string evaluationCriteria)
    {
        _evaluationCriteria = evaluationCriteria ?? throw new ArgumentNullException(nameof(evaluationCriteria));
        return this;
    }

    public ILabwork Build()
    {
        if (string.IsNullOrEmpty(_name))
            throw new InvalidOperationException("Name is required.");
        if (string.IsNullOrEmpty(_description))
            throw new InvalidOperationException("Description is required.");
        if (!_numOfScores.HasValue)
            throw new InvalidOperationException("NumOfScores is required.");

        return new Labwork(
            _author ?? throw new ArgumentNullException(),
            _numOfScores.Value,
            _name,
            _description,
            _evaluationCriteria ?? throw new ArgumentNullException());
    }
}
