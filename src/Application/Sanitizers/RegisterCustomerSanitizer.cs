using Application.DTOs;

namespace Application.Sanitizers;

public static class RegisterCustomerSanitizer
{
    public static RegisterCustomerRequest Sanitize(RegisterCustomerRequest reqcest)
    {
        return new RegisterCustomerRequest(
            Fullname: reqcest.Fullname.Trim(),
            Email: reqcest.Email.Trim().ToLower(),
            Password: reqcest.Password.Trim(),
            NationalId: reqcest.NationalId.Replace(".", "").Replace("-", "").Trim(),
            BirthDate: reqcest.BirthDate,
            Phone: reqcest.Phone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim(),
            ProfilePhoto: reqcest.ProfilePhoto
        );
    }
}
