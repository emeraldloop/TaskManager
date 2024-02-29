namespace TaskManager.Domain.Exceptions;

public class NotFoundException
    : DomainBaseException
{
    public NotFoundException(string message, string? publicMessage = null)
        : base(message, publicMessage)
    {
    }
}