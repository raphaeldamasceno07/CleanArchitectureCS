namespace Domain.Exceptions;

public class DomainValidationException : Domain
{
    public DomainValidationException(string message) : base(message) { }
}