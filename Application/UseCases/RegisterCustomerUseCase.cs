using Application.DTOs;
using Application.Interfaces;
using Application.Sanitizers;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using FluentValidation;

namespace Application.UseCases;

public class RegisterCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IValidator<RegisterCustomerRequest> _validator;

    public RegisterCustomerUseCase(ICustomerRepository customerRepository, IPasswordHasher passwordHasher, IValidator<RegisterCustomerRequest> validator)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
        _validator = validator;
    }

    public async Task<RegisterCustomerResponse> ExecuteAsync(RegisterCustomerRequest request)
    {
        var sanitizedRequest = RegisterCustomerSanitizer.Sanitize(request);

        var validationResult = await _validator.ValidateAsync(sanitizedRequest);

        if (!validationResult.IsValid)
            throw new DomainValidationException(validationResult.ToString());

        var exists = await _customerRepository.GetByEmailAsync(sanitizedRequest.Email);

        if (exists != null)
            throw new CustomerAlreadyExistsException(sanitizedRequest.Email);

        var hash = _passwordHasher.Hash(sanitizedRequest.Password);

        var customer = new Customer(sanitizedRequest.FullName, sanitizedRequest.Email, sanitizedRequest.NationalId, sanitizedRequest.BirthDate, sanitizedRequest.Phone, hash, sanitizedRequest.ProfilePhoto);

        await _customerRepository.AddAsync(customer);

        return new RegisterCustomerResponse(
            customer.Id,
            customer.FullName,
            customer.Email,
            customer.NationalId,
            customer.Phone,
            customer.BirthDate,
            customer.ProfilePhoto
        );
    }
}
