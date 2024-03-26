using FluentValidation;
using MediatR;
using TestInitProject.Application.Common.Interfaces;
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
    private readonly IApplicationDbContext _context;
    private readonly IValidator<CreateCustomerCommand> _validator;
    public CreateCustomerCommandHandler(
        IApplicationDbContext context,
        IValidator<CreateCustomerCommand> validator)
    {
        _context = context;
        _validator = validator;
    }
    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Customer(Guid.NewGuid()).Create(request.Email!, request.Name!);

        entity.AddDomainEvent(new CustomerCreatedEvent(entity));

        _context.Customers.Add(entity);
        
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
