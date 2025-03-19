using Itmo.ObjectOrientedProgramming.Lab3.Addressees;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Loggers;

public class LoggerDecoratorAddressee<T>(T addressee, ILogger logger) : IAddressee where T : class, IAddressee
{
    private readonly T _addressee = addressee;
    private readonly ILogger _logger = logger;

    public bool TrySendMessage(Message message)
    {
        _logger.Log(message);
        return _addressee.TrySendMessage(message);
    }
}