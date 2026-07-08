namespace Application.DTOs;

public record RegisterCustomerRequest(
    string Fullname,
    string Email,
    string Password,
    string NationalId,
    DateOnly BirthDate,
    string Phone,
    string? ProfilePhoto
);
