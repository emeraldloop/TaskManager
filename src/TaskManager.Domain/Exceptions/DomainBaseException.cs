namespace TaskManager.Domain.Exceptions;

public abstract class DomainBaseException
    : ApplicationException
{
    public string? PublicMessage { get; }

    public DomainBaseException(string message, string? publicMessage = null)
        : base(message)
    {
        PublicMessage = publicMessage;
    }
}