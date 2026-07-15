using Application.DTOs;
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

    public RegisterCustomerUseCase(ICustomerRepository customerRepository, IPasswordHasher passwordHasher)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterCustomerResponse> ExecuteAsync(RegisterCustomerRequest request)
    {
        var exists = await _customerRepository.GetByCpfAsync(request.Cpf);

        if (exists != null)
            throw new CustomerAlreadyExistsException(request.Cpf);

        var hash = _passwordHasher.Hash(request.Password);

        var customer = new Customer(request.Fullname, request.Email, request.Cpf, request.BirthDate, request.Phone, hash, request.ProfilePhoto);

        await _customerRepository.AddAsync(customer);

        return new RegisterCustomerResponse(
            customer.Id,
            customer.FullName,
            customer.Email,
            customer.Cpf,
            customer.Phone,
            customer.BirthDate,
            customer.ProfilePhoto
        );
    }
}
