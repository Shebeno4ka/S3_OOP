using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public interface ILabworkBuilder
{
    ILabworkBuilder WithAuthor(IUser author);

    ILabworkBuilder WithName(string name);

    ILabworkBuilder WithDescription(string description);

    ILabworkBuilder WithNumOfScores(int numOfScores);

    ILabworkBuilder WithEvaluationCriteria(string evaluationCriteria);

    ILabwork Build();
}