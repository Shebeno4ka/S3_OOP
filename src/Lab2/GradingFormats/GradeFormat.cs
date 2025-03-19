namespace Itmo.ObjectOrientedProgramming.Lab2.GradingFormats;

public abstract class GradeFormat
{
    public virtual int GetAddingPoints() => 0;

    public virtual bool CheckPoints(int points) => true;
}