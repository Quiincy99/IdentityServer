using MediatR;
using TestInitProject.Domain;

namespace TestInitProject.Application;

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
