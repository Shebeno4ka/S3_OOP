using Itmo.ObjectOrientedProgramming.Lab3.Addressees;

namespace Itmo.ObjectOrientedProgramming.Lab3.Topics;

public interface ITopic : IAddressee
{
    string Name { get; }
}