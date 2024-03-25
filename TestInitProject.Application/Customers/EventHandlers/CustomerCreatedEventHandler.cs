using MediatR;
using TestInitProject.Domain.Events;

namespace TestInitProject.Application.Customers.EventHandlers;

public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    public CustomerCreatedEventHandler()
    {
        
    }
    public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("CustomerCreatedEventHandler: " + notification.Customer.Email);

        return Task.CompletedTask;
    }
}
