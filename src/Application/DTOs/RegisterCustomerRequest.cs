namespace Application.DTOs;

public record RegisterCustomerRequest(
    string Fullname,
    string Email,
    string Password,
    string Cpf,
    DateOnly BirthDate,
    string Phone,
    string? ProfilePhoto
);
