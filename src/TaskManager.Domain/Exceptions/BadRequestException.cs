namespace TaskManager.Domain.Exceptions;

public class BadRequestException
    : DomainBaseException
{
    public BadRequestException(string message, string? publicMessage = null)
        : base(message, publicMessage)
    {
    }
}