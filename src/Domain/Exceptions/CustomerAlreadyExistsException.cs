namespace Domain.Exceptions;

public class CustomerAlreadyExistsException : Domain
{
    public override int StatusCode => 409;

    public CustomerAlreadyExistsException(string cpf)
        : base($"The CPF '{cpf}' is already registered in the system.") { }
}