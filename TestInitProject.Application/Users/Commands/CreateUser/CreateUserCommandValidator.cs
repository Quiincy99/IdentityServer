using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestInitProject.Application.Common.Interfaces;

namespace TestInitProject.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepository _UserRepository;
    public CreateUserCommandValidator(IUserRepository UserRepository)
    {
        _UserRepository = UserRepository;

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
        return await _UserRepository.CheckUniqueEmailAsync(email);
    }
}
