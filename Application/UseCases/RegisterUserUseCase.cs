using Application.DTOs;
using Application.Interfaces;
using Application.Sanitizers;
using Application.Validators.User;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.UseCases;

public class RegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request)
    {
        var sanitizedRequest = RegisterUserSanitizer.Sanitize(request);

        RegisterUserValidator.Validate(sanitizedRequest);

        var exists = await _userRepository.GetByEmailAsync(sanitizedRequest.Email);

        if (exists != null)
            throw new UserAlreadyExistsException(sanitizedRequest.Email);

        var hash = _passwordHasher.Hash(sanitizedRequest.Password);

        var user = new User(sanitizedRequest.Name, sanitizedRequest.Email, hash, sanitizedRequest.ProfilePhoto);

        await _userRepository.AddAsync(user);

        return new RegisterUserResponse(
            user.Id,
            user.Name,
            user.Email,
            user.ProfilePhoto
        );
    }
}