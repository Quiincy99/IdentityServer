using TestInitProject.Domain.Common;
using TestInitProject.Domain.Entities;

namespace TestInitProject.Domain.Events;

public class UserCreatedEvent : BaseEvent
{
    public UserCreatedEvent(User user)
    {
        User = user;
    }

    public User User { get; }
}
