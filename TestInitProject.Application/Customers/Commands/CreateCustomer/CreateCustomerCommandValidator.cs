using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestInitProject.Application.Common.Interfaces;

namespace TestInitProject.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        RuleFor(v => v.Email)
            .NotEmpty()
            .MaximumLength(100)
            .MustAsync(BeUniqueEmail)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(100);
    }

    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _customerRepository.CheckUniqueEmailAsync(email);
    }
}
