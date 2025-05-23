﻿using MediatR;
using IdentityServer.Domain.Events;

namespace IdentityServer.Application.Users.EventHandlers;

public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    public UserCreatedEventHandler()
    {

    }
    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("UserCreatedEventHandler: " + notification.User.Email);

        return Task.CompletedTask;
    }
}
