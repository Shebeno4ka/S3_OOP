using Itmo.ObjectOrientedProgramming.Lab4.CommandExecutors;
using Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileOutputers;
using Itmo.ObjectOrientedProgramming.Lab4.FsVisitors;
using Itmo.ObjectOrientedProgramming.Lab4.States;
using Xunit;

namespace Lab4.Tests;

public class ProgramDefaultConfigurationTests
{
    internal static class TestData
{
    public static string[] ConnectCommand { get; } = ["connect", "C:\\path", "-m", "local"];

    public static string[] DisconnectCommand { get; } = ["disconnect"];

    public static string[] TreeGotoCommand { get; } = ["tree", "goto", "directory/"];

    public static string[] TreeListCommand { get; } = ["tree", "list", "-d", "3"];

    public static string[] FileShowCommand { get; } = ["file", "show", "file.txt", "-m", "console"];

    public static string[] FileMoveCommand { get; } = ["file", "move", "source.txt", "destination/"];

    public static string[] FileCopyCommand { get; } = ["file", "copy", "source.txt", "destination/"];

    public static string[] FileDeleteCommand { get; } = ["file", "delete", "file.txt"];

    public static string[] FileRenameCommand { get; } = ["file", "rename", "file.txt", "new_name.txt"];
}

    private static readonly LocalConnectionState State = new LocalConnectionState();
    private static readonly CommandHandlerBase Handlers = new LocalConnectCommandHandler(State)
        .AddNext(new DisconnectCommandHandler(State))
        .AddNext(new TreeGotoCommandHandler(State))
        .AddNext(new TreeListCommandHandler(State, new ConsoleFsComponentVisitor()))
        .AddNext(new FileShowCommandHandler(State, new FileOutputerFactory()))
        .AddNext(new FileMoveCommandHandler(State))
        .AddNext(new FileCopyCommandHandler(State))
        .AddNext(new FileDeleteCommandHandler(State))
        .AddNext(new FileRenameCommandHandler(State));

    // This method provides the test data for MemberData
    public static IEnumerable<object[]> GetTestCommands()
    {
        yield return new object[] { TestData.ConnectCommand, typeof(LocalConnectCommandExecutor) };
        yield return new object[] { TestData.DisconnectCommand, typeof(DisconnectCommandExecutor) };
        yield return new object[] { TestData.TreeGotoCommand, typeof(TreeGotoCommandExecutor) };
        yield return new object[] { TestData.TreeListCommand, typeof(TreeListCommandExecutor) };
        yield return new object[] { TestData.FileShowCommand, typeof(FileShowCommandExecutor) };
        yield return new object[] { TestData.FileMoveCommand, typeof(FileMoveCommandExecutor) };
        yield return new object[] { TestData.FileCopyCommand, typeof(FileCopyCommandExecutor) };
        yield return new object[] { TestData.FileDeleteCommand, typeof(FileDeleteCommandExecutor) };
        yield return new object[] { TestData.FileRenameCommand, typeof(FileRenameCommandExecutor) };
    }

    // Use MemberData instead of InlineData
    [Theory]
    [MemberData(nameof(GetTestCommands))]
    public void ParseShouldReturnCorrectExecutor(string[] args, Type expectedType)
    {
        // Act
        ICommandExecutor? executor = Handlers.Handle(args);

        // Assert
        Assert.NotNull(executor);
        Assert.IsType(expectedType, executor);
    }
}
