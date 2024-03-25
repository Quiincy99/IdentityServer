using TestInitProject.Domain.Common;
using TestInitProject.Domain.Customers;

namespace TestInitProject.Domain.Events;

public class CustomerCreatedEvent : BaseEvent
{
    public CustomerCreatedEvent(Customer customer)
    {
        Customer = customer;        
    }

    public Customer Customer { get; }
}
