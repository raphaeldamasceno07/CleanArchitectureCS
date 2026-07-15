using Application.DTOs;
using Application.Sanitizers;
using Application.UseCases;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/users/register")]
public class CustomersController : ControllerBase
{
    public readonly RegisterCustomerUseCase _registerCustomerUseCase;
    public readonly IValidator<RegisterCustomerRequest> _validator;

    public CustomersController(RegisterCustomerUseCase registerCustomerUseCase, IValidator<RegisterCustomerRequest> validator)
    {
        _registerCustomerUseCase = registerCustomerUseCase;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterCustomerRequest request)
    {
        var sanitizedRequest = RegisterCustomerSanitizer.Sanitize(request);

        var validationResult = await _validator.ValidateAsync(sanitizedRequest);

        if (!validationResult.IsValid)
            throw new DomainValidationException(validationResult.ToString());

        var response = await _registerCustomerUseCase.ExecuteAsync(request);

        return Created($"/api/users/{response.Id}", response);
    }
}
