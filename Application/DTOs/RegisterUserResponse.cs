using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs;

public record RegisterUserResponse(
  Guid Id,
  string Name,
  string Email,
  string? ProfilePhoto
);
