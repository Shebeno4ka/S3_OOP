namespace Itmo.ObjectOrientedProgramming.Lab4.FileOutputers;

public interface IFileOutputerFactory
{
    IFileOutputer Create(string mode);
}