using Application.DTOs;
using Application.UseCases;
using Domain.Exceptions;
using Infrastructure.Repositories;
using UnitTest.Fakes;
using Xunit; // Test framework

namespace UnitTest.UseCases.User;

public class RegisterUserUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldCreateUser_WhenDataIsValid()
    {
        var repository = new InMemoryUserRepository();
        var hasher = new FakePasswordHasher();
        var useCase = new RegisterUserUseCase(repository, hasher);

        var request = new RegisterUserRequest
        {
            Name = "Raphael",
            Email = "raphael@teste.com",
            Password = "very_strong_password",
            ProfilePhoto = null
        };

        var response = await useCase.ExecuteAsync(request);

        Assert.NotNull(response);
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal("raphael@teste.com", response.Email);
        Assert.Equal("Raphael", response.Name);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowException_WhenEmailAlreadyExists()
    {
        // 1. Arrange
        var repository = new InMemoryUserRepository();
        var hasher = new FakePasswordHasher();
        var useCase = new RegisterUserUseCase(repository,hasher);

        var request = new RegisterUserRequest
        {
            Name = "Raphael",
            Email = "raphael@teste.com",
            Password = "senha_super_forte"
        };

        await useCase.ExecuteAsync(request);

        var exception = await Assert.ThrowsAsync<UserAlreadyExistsException>(
            () => useCase.ExecuteAsync(request)
        );

        Console.WriteLine(exception.Message );

        Assert.Contains($"The email '{request.Email}' is already registered in the system.", exception.Message);
    }
}