using System;

namespace Domain.Exceptions;

public abstract class Domain : Exception
{

    public virtual int StatusCode => 400;

    protected Domain(string message) : base(message) { }
}