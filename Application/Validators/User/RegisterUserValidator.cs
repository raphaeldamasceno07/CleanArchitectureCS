using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.User;

public static class RegisterUserValidator
{
    public static void Validate(RegisterUserRequest request)
    {
        ValidateName(request.Name);

        ValidateEmail(request.Email);

        ValidatePassword(request.Password);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");

        if (name.Length < 2)
            throw new ArgumentException("Name must have at least 2 characters");

        if (name.Any(char.IsDigit))
            throw new ArgumentException("Name cannot contain numbers");
    }

    private static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        if (!email.Contains("@") || email.StartsWith("@") || email.EndsWith("@"))
            throw new ArgumentException("Invalid email");
    }

    private static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password is required");

        if (password.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters");

        if (!password.Any(char.IsUpper))
            throw new ArgumentException("Password must contain uppercase");

        if (!password.Any(char.IsLower))
            throw new ArgumentException("Password must contain lowercase");

        if (!password.Any(char.IsDigit))
            throw new ArgumentException("Password must contain number");

        if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            throw new ArgumentException("Password must contain special char");
    }
}