namespace Application.DTOs;

public record RegisterCustomerResponse(
  Guid Id,
  string Fullname,
  string Email,
  string Cpf,
  string Phone,
  DateOnly BirthDate,
  string? ProfilePhoto
);
