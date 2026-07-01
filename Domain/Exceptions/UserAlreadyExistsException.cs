using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions;

public class UserAlreadyExistsException:DomainException
{
    public UserAlreadyExistsException(string email) : base($"The email '{email}' is already registered in the system.") { }
}
