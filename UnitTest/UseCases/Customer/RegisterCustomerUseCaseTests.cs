using Application.DTOs;
using Application.UseCases;
using Application.Validators.Customer;
using Domain.Exceptions;
using Infrastructure.Repositories;
using UnitTest.Fakes;

namespace UnitTest.UseCases.Customer;

public class RegisterCustomerUseCaseTests
{

    private readonly InMemoryCustomerRepository _customerRepository;
    private readonly FakePasswordHasher _passwordHasher;
    private readonly RegisterCustomerValidator _validator;
    private readonly RegisterCustomerUseCase _useCase;

    public RegisterCustomerUseCaseTests()
    {
        _customerRepository = new InMemoryCustomerRepository();
        _passwordHasher = new FakePasswordHasher();
        _validator = new RegisterCustomerValidator();
        _useCase = new RegisterCustomerUseCase(_customerRepository, _passwordHasher, _validator);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreateCustomer_WhenDataIsValid()
    {

        var birthDate = DateOnly.Parse("1990-01-01");

        var request = new RegisterCustomerRequest
      (
          FullName: "Raphael",
          Email: "raphael@teste.com",
          Password: "senha_super_Forte1@",
          NationalId: "123.687.987-12",
          BirthDate: birthDate,
          Phone: "(11) 98765-4321",
          ProfilePhoto: null
      );

        var response = await _useCase.ExecuteAsync(request);

        Assert.NotNull(response);
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal("raphael@teste.com", response.Email);
        Assert.Equal("Raphael", response.Fullname);
        Assert.Equal("123.687.987-12", response.NationalId);
        Assert.Equal(birthDate, response.BirthDate);
        Assert.Equal("(11) 98765-4321", response.Phone);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowException_WhenEmailAlreadyExists()
    {
        var birthDate = DateOnly.Parse("1990-01-01");

        var request = new RegisterCustomerRequest
        (
            FullName: "Raphael",
            Email: "raphael@teste.com",
            Password: "senha_super_Forte1@",
            BirthDate: birthDate,
            NationalId: "123.687.987-12",
            Phone: "(11) 98765-4321",
            ProfilePhoto: null
        );

        await _useCase.ExecuteAsync(request);

        var exception = await Assert.ThrowsAsync<CustomerAlreadyExistsException>(
            () => _useCase.ExecuteAsync(request)
        );

        Assert.Contains($"The email '{request.Email}' is already registered in the system.", exception.Message);
    }

    [Theory]
    [InlineData("", "raphael@test.com", "Password123!", "12345678901", "2000-01-01", "(11) 98765-4321")]
    [InlineData("R", "raphael@test.com", "Password123!", "12345678901", "2000-01-01", "(11) 98765-4321")]
    [InlineData("Raphael", "invalid-email", "Password123!", "12345678901", "2000-01-01", "(11) 98765-4321")]
    [InlineData("Raphael", "", "Password123!", "12345678901", "2000-01-01", "(11) 98765-4321")]
    [InlineData("Raphael", "raphael@test.com", "", "12345678901", "2000-01-01", "(11) 98765-4321")]
    [InlineData("Raphael", "raphael@test.com", "password", "12345678901", "2000-01-01", "(11) 98765-4321")]
    [InlineData("Raphael", "raphael@test.com", "Password123!", "", "2000-01-01", "(11) 98765-4321")]
    [InlineData("Raphael", "raphael@test.com", "Password123!", "12345", "2000-01-01", "(11) 98765-4321")]
    [InlineData("Raphael", "raphael@test.com", "Password123!", "12345678901", "2020-01-01", "(11) 98765-4321")]
    [InlineData("Raphael", "raphael@test.com", "Password123!", "12345678901", "2000-01-01", "")]
    [InlineData("Raphael", "raphael@test.com", "Password123!", "12345678901", "2000-01-01", "invalid-phone")]
    public async Task ExecuteAsync_ShouldThrowException_WhenDataIsInvalid(string fullName, string email, string password, string nationalId, string birthDateString, string phone)
    {
        var birthDate = DateOnly.Parse(birthDateString);
        var request = new RegisterCustomerRequest(
            FullName: fullName,
            Email: email,
            Password: password,
            NationalId: nationalId,
            BirthDate: birthDate,
            Phone: phone,
            ProfilePhoto: ""
        );

        await Assert.ThrowsAsync<ArgumentException>(() => _useCase.ExecuteAsync(request));
    }
}