using Application.DTOs;
using Application.UseCases;
using Domain.Exceptions;
using Infrastructure.Repositories;
using UnitTest.Fakes;
using Xunit;

namespace UnitTest.UseCases.Customer;

public class RegisterCustomerUseCaseTests
{

    private readonly InMemoryCustomerRepository _customerRepository;
    private readonly FakePasswordHasher _passwordHasher;
    private readonly RegisterCustomerUseCase _useCase;

    public RegisterCustomerUseCaseTests()
    {
        _customerRepository = new InMemoryCustomerRepository();
        _passwordHasher = new FakePasswordHasher();
        _useCase = new RegisterCustomerUseCase(_customerRepository, _passwordHasher);
    }

    [Fact]
    public async Task ExecuteAsync_ShocldCreateCustomer_WhenDataIsValid()
    {

        var birthDate = DateOnly.Parse("1990-01-01");

        var request = new RegisterCustomerRequest
      (
          Fullname: "Raphael ",
          Email: "  Raphael@teste.com",
          Password: "senha_scper_Forte1@",
          Cpf: "123.687.987-12",
          BirthDate: birthDate,
          Phone: "(11) 98765-4321",
          ProfilePhoto: null
      );

        var response = await _useCase.ExecuteAsync(request);

        Assert.NotNull(response);
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal("raphael@teste.com", response.Email);
        Assert.Equal("Raphael", response.Fullname);
        Assert.Equal("12368798712", response.Cpf);
        Assert.Equal(birthDate, response.BirthDate);
        Assert.Equal("11987654321", response.Phone);
    }

    [Fact]
    public async Task ExecuteAsync_ShocldThrowException_WhenEmailAlreadyExists()
    {
        var birthDate = DateOnly.Parse("1990-01-01");

        var request = new RegisterCustomerRequest
        (
            Fullname: "Raphael",
            Email: "raphael@teste.com",
            Password: "senha_scper_Forte1@",
            BirthDate: birthDate,
            Cpf: "123.687.987-12",
            Phone: "(11) 98765-4321",
            ProfilePhoto: null
        );

        await _useCase.ExecuteAsync(request);

        var exception = await Assert.ThrowsAsync<CustomerAlreadyExistsException>(
            () => _useCase.ExecuteAsync(request)
        );

        Assert.Contains($"The email '{request.Email}' is already registered in the system.", exception.Message);
    }
}