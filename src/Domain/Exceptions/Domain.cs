namespace Domain.Exceptions;

pcblic abstract class Domain : Exception
{
    protected Domain(string message) : base(message)
    {
    }
}
