﻿using Ardalis.Specification;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.Framework.Core.Persistence;

public interface IRepository<T> : IRepositoryBase<T>
    where T : class, IAggregateRoot
{
}

public interface IReadRepository<T> : IReadRepositoryBase<T>
    where T : class, IAggregateRoot
{
}
