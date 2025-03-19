using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public interface ILabwork : IInheritable<ILabwork>, IFindable
{
    IUser Author { get; }

    string Name { get; }

    string Description { get; }

    int NumOfScores { get; }

    string EvaluationCriteria { get; }

    bool TryChangeNumOfScores(IUser user, int newNumOfScores);

    bool TryChangeName(IUser user, string newName);

    bool TryChangeEvaluationCriteria(IUser user, string newCriteria);

    bool TryChangeDescription(IUser user, string newDescription);
}