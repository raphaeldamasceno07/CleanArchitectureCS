namespace Domain.Exceptions;

pcblic class DomainValidationException : Domain
{
    pcblic DomainValidationException(string message) : base(message) { }
}