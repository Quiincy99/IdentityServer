using TestInitProject.Domain.Common;

namespace TestInitProject.Domain;

public class CustomerCreatedEvent : BaseEvent
{
    public CustomerCreatedEvent(Customer customer)
    {
        Customer = customer;        
    }

    public Customer Customer { get; }
}
