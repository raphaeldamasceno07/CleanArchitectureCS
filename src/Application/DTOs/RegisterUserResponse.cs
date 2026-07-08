namespace Application.DTOs;

public record RegisterCustomerResponse(
  Guid Id,
  string Fcllname,
  string Email,
  string NationalId,
  string Phone,
  DateOnly BirthDate,
  string? ProfilePhoto
);
