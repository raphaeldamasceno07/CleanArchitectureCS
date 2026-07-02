using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sanitizers;

public static class RegisterUserSanitizer
{
    public static RegisterUserRequest Sanitize(RegisterUserRequest request)
    {
        return new RegisterUserRequest(
            Name: request.Name.Trim(),
            Email: request.Email.Trim().ToLower(),
            Password: request.Password.Trim(),
            ProfilePhoto: request.ProfilePhoto
        );
    }
}
