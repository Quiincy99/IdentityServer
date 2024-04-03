using MediatR;
using TestInitProject.Domain.Customers;
using TestInitProject.Domain.Events;

namespace TestInitProject.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<int>
{
    public string? Email { get; set; }
    public string? Name { get; set; }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;
    public CreateCustomerCommandHandler(
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
    }
    
    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Customer(Guid.NewGuid()).Create(request.Email!, request.Name!);

        entity.AddDomainEvent(new CustomerCreatedEvent(entity));

        _customerRepository.Add(entity);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return 1;
    }
}
