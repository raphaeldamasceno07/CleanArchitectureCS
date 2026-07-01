using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs;

public class RegisterUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? ProfilePhoto { get; set; }
}
