﻿using MediatR;

namespace Skittles.Framework.Core.Domain.Events;

public abstract record DomainEvent : IDomainEvent, INotification
{
    public DateTime RaisedOn { get; protected set; } = DateTime.UtcNow;
}
