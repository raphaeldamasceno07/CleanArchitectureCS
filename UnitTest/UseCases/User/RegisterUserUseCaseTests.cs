using Application.DTOs;
using Application.UseCases;
using Domain.Exceptions;
using Infrastructure.Repositories;
using UnitTest.Fakes;

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
      (
          Name: "Raphael",
          Email: "raphael@teste.com",
          Password: "senha_super_Forte1@",
          ProfilePhoto: null
      );

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
        var useCase = new RegisterUserUseCase(repository, hasher);

        var request = new RegisterUserRequest
        (
            Name: "Raphael",
            Email: "raphael@teste.com",
            Password: "senha_super_Forte1@",
            ProfilePhoto: null
        );

        await useCase.ExecuteAsync(request);

        var exception = await Assert.ThrowsAsync<UserAlreadyExistsException>(
            () => useCase.ExecuteAsync(request)
        );

        Assert.Contains($"The email '{request.Email}' is already registered in the system.", exception.Message);
    }

    [Theory]
    [InlineData("", "raphael@test.com", "Password123!")]
    [InlineData("R", "raphael@test.com", "Password123!")]
    [InlineData("Raphael", "invalid-email", "Password123!")]
    [InlineData("Raphael", "", "Password123!")]
    [InlineData("Raphael", "raphael@test.com", "")]
    [InlineData("Raphael", "raphael@test.com", "password")]
    public async Task ExecuteAsync_ShouldThrowException_WhenDataIsInvalid(string name, string email, string password)
    {
        var repository = new InMemoryUserRepository();
        var hasher = new FakePasswordHasher();
        var useCase = new RegisterUserUseCase(repository, hasher);

        var request = new RegisterUserRequest(Name: name, Email: email, Password: password, ProfilePhoto: "");

        await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(request));
    }
}