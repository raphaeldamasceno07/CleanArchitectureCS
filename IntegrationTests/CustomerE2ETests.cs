using Application.DTOs;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests;

public class CustomerE2ETests : IntegrationTestBase
{
    private RegisterCustomerRequest CreateValidRequest() => new(
        "Raphael Damasceno",
        $"rapha.{Guid.NewGuid()}@teste.com",
        "Password123!",
        "04443484005",
        new DateOnly(1994, 10, 31),
        "31999999999",
        ""
    );

    [Fact]
    public async Task Register_Should_Return_201_And_Persist_Customer()
    {
        // Arrange
        var request = CreateValidRequest();

        // Act - Cadastra
        var response = await Client.PostAsJsonAsync("/api/users/register", request);

        // Assert - Status de Criação
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Opcional e Recomendado: Validar se o dado está acessível via API
        var result = await response.Content.ReadFromJsonAsync<RegisterCustomerResponse>();
        result.Should().NotBeNull();
        result!.Email.Should().Be(request.Email.ToLower().Trim());
    }


    [Fact]
    public async Task Register_Should_Return_400BadRequest_When_FluentValidation_Fails()
    {
        // Arrange 
        var invalidRequest = new RegisterCustomerRequest(
            "",
            "email-invalido",
            "123",
            "123",
            new DateOnly(2025, 1, 1),
            "fone",
            ""
        );

        // Act
        var response = await Client.PostAsJsonAsync("/api/users/register", invalidRequest);
        var errorContent = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(
            HttpStatusCode.BadRequest,
            because: "O pipeline do ASP.NET Core deveria barrar a requisição antes do UseCase."
        );

        // Se você usa ProblemDetails ou retoma um padrão de erros, pode inspecionar a mensagem:
        errorContent.Should().Contain("Fullname").And.Contain("Email");
    }

    [Fact]
    public async Task Register_Should_Return_409Conflict_When_Customer_Already_Exists()
    {
        // Arrange
        var request = CreateValidRequest();

        // Act 1 - cria o usuário (deve dar 201)
        var firstResponse = await Client.PostAsJsonAsync("/api/users/register", request);
        firstResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        // Act 2 - tenta criar novamente com o mesmo email
        var secondResponse = await Client.PostAsJsonAsync("/api/users/register", request);
        var errorContent = await secondResponse.Content.ReadAsStringAsync();

        // Assert
        secondResponse.StatusCode.Should().Be(HttpStatusCode.Conflict);

        errorContent.Should().Contain("The CPF").And.Contain("is already registered in the system.");
    }
}