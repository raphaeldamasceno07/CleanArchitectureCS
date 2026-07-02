using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs;

public record RegisterUserRequest(
    string Name,
    string Email,
    string Password,
    string? ProfilePhoto
);
