namespace TaskManager.Domain.Exceptions;

public class DomainException
    : DomainBaseException
{
    public DomainException(string message, string? publicMessage = null)
        : base(message, publicMessage)
    {
    }
}