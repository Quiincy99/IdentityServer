using MediatR;
using TestInitProject.Domain.Entities;
using TestInitProject.Domain.Events;

namespace TestInitProject.Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<int>
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _UserRepository;
    public CreateUserCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository UserRepository)
    {
        _unitOfWork = unitOfWork;
        _UserRepository = UserRepository;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new User(Guid.NewGuid()).Create(request.Email!, request.Name!);

        entity.AddDomainEvent(new UserCreatedEvent(entity));

        _UserRepository.Add(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return 1;
    }
}
