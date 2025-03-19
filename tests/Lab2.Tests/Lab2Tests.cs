using Itmo.ObjectOrientedProgramming.Lab2.GradingFormats;
using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using Xunit;

namespace Lab2.Tests;

public class Lab2Tests
{
    [Fact]
    public void SubjectWith100ScoresInSumShouldBeCreated()
    {
        // Arrange
        var admin = new User("John");
        ILabwork testLab = new LabworkBuilder().WithName("LAB").WithAuthor(admin).WithDescription("xyz").WithNumOfScores(90).WithEvaluationCriteria("12345").Build();
        var subjectBuilder = new SubjectBuilder();
        string subjectName = "OOP";
        ISubjectBuilder builder = subjectBuilder.WithAuthor(admin).WithName(subjectName).WithGradingFormat(new ExamGradeFormat(10)).AddLab(testLab);

        // Act
        Subject testSubject = builder.Build();

        // Assert
        Assert.True(testSubject.Labs.Contains(testLab));
        Assert.Equal(testSubject.Name, subjectName);
    }

    [Fact]
    public void TryOfChangingLabworkByAuthorShouldPass()
    {
        // Arrange
        var author = new User("Admin");
        ILabwork testLab = new LabworkBuilder().WithName("LAB").WithAuthor(author).WithDescription("xyz").WithNumOfScores(10).WithEvaluationCriteria("12345").Build();

        // Act & Assert
        Assert.True(testLab.TryChangeName(author, "LAB1"));
    }

    [Fact]
    public void TryOfChangingLabworkNotByAuthorShouldFail()
    {
        // Arrange
        var author = new User("Admin");
        ILabwork testLab = new LabworkBuilder().WithName("LAB").WithAuthor(author).WithDescription("xyz").WithNumOfScores(10).WithEvaluationCriteria("12345").Build();
        var wrongAuthor = new User("Admin");

        // Act & Assert
        Assert.False(testLab.TryChangeName(wrongAuthor, "Lab1"));
    }

    [Fact]
    public void ObjectsCreatedFromAlreadyExistentShouldContainParentIDs()
    {
        // Arrange
        var author = new User("John");
        var parentLabwork = new Labwork(author, 10, "Lab1", "Just a lab", "Do anything.");

        // Act
        ILabwork childLabwork = parentLabwork.Inherit();

        // Assert
        Assert.Equal(parentLabwork.Id, childLabwork.OriginalId);
    }
}