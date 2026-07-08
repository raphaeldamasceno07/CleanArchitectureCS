using Application.DTOs;
using FluentValidation;

namespace Application.Validators.Customer;

public class RegisterCustomerValidator : AbstractValidator<RegisterCustomerRequest>
{
    public RegisterCustomerValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(c => c.FullName)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MinimumLength(2)
            .WithMessage("Name must be at least 2 characters long.")
            .MaximumLength(70)
            .WithMessage("Name must not exceed 70 characters.")
            .Matches(@"^[\p{L}\s'\-]+$")
            .WithMessage("Name can only contain letters, spaces, hyphens, and apostrophes.");

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.");

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]")
            .WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d")
            .WithMessage("Password must contain at least one number.");

        RuleFor(c => c.NationalId)
            .NotEmpty()
            .WithMessage("National ID is required.")
            .Length(11, 14)
            .WithMessage("National ID must be between 11 and 14 digits.");

        RuleFor(c => c.BirthDate)
           .NotEmpty()
           .WithMessage("Birth date is required.")
           .Must(BeAValidAge)
           .WithMessage("Customer must be at least 18 years old.");

        RuleFor(c => c.Phone)
           .NotEmpty()
           .WithMessage("Phone number is required.")
           .Matches(@"^\+?[1-9]\d{1,14}$")
           .WithMessage("Phone number must be a valid international phone number.");

        RuleFor(c => c.ProfilePhoto)
            .Must(photo => string.IsNullOrEmpty(photo) || IsValidImageUrl(photo))
            .WithMessage("ProfilePhoto must be a valid URL or empty.");
    }

    private static bool IsValidImageUrl(string? url)
    {
        if (string.IsNullOrEmpty(url))
            return true;

        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private static bool BeAValidAge(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age)) age--;

        return age >= 18;
    }
}