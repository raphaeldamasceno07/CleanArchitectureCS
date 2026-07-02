namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHashed { get; private set; }
    public string? ProfilePhoto { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected User() { }

    public User(string name, string email, string passwordHashed, string? profilePhoto = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Invalid name");

        if (!email.Contains("@"))
            throw new ArgumentException("Invalid email");

        if (string.IsNullOrWhiteSpace(passwordHashed))
            throw new ArgumentException("Invalid password");

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PasswordHashed = passwordHashed;
        ProfilePhoto = profilePhoto;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProfilePhoto(string photoUrl)
    {
        ProfilePhoto = photoUrl;
        UpdatedAt = DateTime.UtcNow;
    }
}
