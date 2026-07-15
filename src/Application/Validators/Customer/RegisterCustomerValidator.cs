using Application.DTOs;
using FluentValidation;

namespace Application.Validators.Customer;

public class RegisterCustomerValidator : AbstractValidator<RegisterCustomerRequest>
{
    public RegisterCustomerValidator()
    {

        RuleFor(c => c.Fullname)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Fullname is required.")
            .MinimumLength(2)
            .WithMessage("Fullname must be at least 2 characters long.")
            .MaximumLength(70)
            .WithMessage("Fullname must not exceed 70 characters.")
            .Matches(@"^[\p{L}\s'\-]+$")
            .WithMessage("Fullname can only contain letters, spaces, hyphens, and apostrophes.");

        RuleFor(c => c.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.");

        RuleFor(c => c.Password)
            .Cascade(CascadeMode.Stop)
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

        RuleFor(c => c.Cpf)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Cpf is required.")
            .Length(11)
            .WithMessage("Cpf must be 11 characters.");

        RuleFor(c => c.BirthDate)
           .Cascade(CascadeMode.Stop)
           .NotEmpty()
           .WithMessage("Birth date is required.")
           .Must(BeAValidAge)
           .WithMessage("Customer must be at least 18 years old.");

        RuleFor(c => c.Phone)
           .Cascade(CascadeMode.Stop)
           .NotEmpty()
           .WithMessage("Phone number is required.")
           .Matches(@"^\+?[1-9]\d{1,14}$")
           .WithMessage("Phone number must be a valid intercpf phone number.");

        RuleFor(c => c.ProfilePhoto)
            .Must(photo => string.IsNullOrEmpty(photo) || IsValidImageURL(photo))
            .WithMessage("ProfilePhoto must be a valid URL or empty.");
    }

    private static bool IsValidImageURL(string? URL)
    {
        if (string.IsNullOrEmpty(URL))
            return true;

        return Uri.TryCreate(URL, UriKind.Absolute, out var uriResult)
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