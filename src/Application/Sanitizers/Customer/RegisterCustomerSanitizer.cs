using Application.DTOs;

namespace Application.Sanitizers;

public static class RegisterCustomerSanitizer
{
    public static RegisterCustomerRequest Sanitize(RegisterCustomerRequest request)
    {
        return new RegisterCustomerRequest(
            Fullname: request.Fullname.Trim(),
            Email: request.Email.Trim().ToLower(),
            Password: request.Password.Trim(),
            Cpf: request.Cpf.Replace(".", "").Replace("-", "").Trim(),
            BirthDate: request.BirthDate,
            Phone: request.Phone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim(),
            ProfilePhoto: request.ProfilePhoto
        );
    }
}
