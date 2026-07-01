using Application.DTOs;
using Application.Interfaces;
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
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new UserAlreadyExistsException(request.Email);
        }

        string passwordHash = _passwordHasher.Hash(request.Password);

        var user = new User(
            name: request.Name,
            email: request.Email,
            passwordHashed: passwordHash,
            profilePhoto: request.ProfilePhoto
        );

        await _userRepository.AddAsync(user);

        return new RegisterUserResponse(
            Id: user.Id,
            Name: user.Name,
            Email: user.Email,
            ProfilePhoto: user.ProfilePhoto
        );
    }
}