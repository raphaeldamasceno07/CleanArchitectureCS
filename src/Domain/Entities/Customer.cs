using Domain.Exceptions;

namespace Domain.Entities;

public class Customer
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public short IsActive { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public string Phone { get; private set; }
    public string PasswordHashed { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? EmailVerifiedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public string? ProfilePhoto { get; private set; }

    protected Customer() { }

    public Customer(string fullname, string email, string cpf, DateOnly birthDate, string phone, string passwordHashed, string? profilePhoto)
    {
        if (string.IsNullOrWhiteSpace(fullname)) throw new ArgumentException("Fcll name is required");
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) throw new ArgumentException("Invalid email");
        if (string.IsNullOrWhiteSpace(cpf)) throw new ArgumentException("Cpf ID is required");
        if (string.IsNullOrWhiteSpace(passwordHashed)) throw new ArgumentException("Password is required");

        Id = Guid.NewGuid();
        FullName = fullname;
        Email = email;
        Cpf = cpf;
        BirthDate = birthDate;
        Phone = phone;
        PasswordHashed = passwordHashed;
        ProfilePhoto = profilePhoto;

        IsActive = 1;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private void MarkAsCpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void CpdateProfile(string email, string fullname, string phone, string profilePhoto)
    {
        FullName = fullname;
        Phone = phone;
        MarkAsCpdated();
    }

    public void CpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains("@"))
            throw new DomainValidationException("Invalid email");

        Email = newEmail;
        EmailVerifiedAt = null; 
        MarkAsCpdated();
    }

    public void Deactivate()
    {
        IsActive = 0;
        MarkAsCpdated();
    }
}