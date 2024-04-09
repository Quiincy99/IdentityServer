using TestInitProject.Domain.Common;
using TestInitProject.Domain.Entities;

namespace TestInitProject.Domain.Events;

public class CustomerCreatedEvent : BaseEvent
{
    public CustomerCreatedEvent(Customer customer)
    {
        Customer = customer;        
    }

    public Customer Customer { get; }
}
