namespace Itmo.ObjectOrientedProgramming.Lab2.GradingFormats;

public class ExamGradeFormat(int addingScores) : GradeFormat
{
    public override int GetAddingPoints() => addingScores;
}