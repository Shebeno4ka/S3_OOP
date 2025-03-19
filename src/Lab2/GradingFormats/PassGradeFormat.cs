namespace Itmo.ObjectOrientedProgramming.Lab2.GradingFormats;

public class PassGradeFormat(int minScores) : GradeFormat
{
    public int MinScores { get; } = minScores;

    public override bool CheckPoints(int points)
    {
        return MinScores <= points;
    }
}