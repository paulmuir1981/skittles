using Ardalis.Specification.EntityFrameworkCore;
using Skittles.Framework.Core.Domain.Contracts;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Infrastructure.Persistence;

namespace Skittles.WebApi.Infrastructure;

internal sealed class SkittlesRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot
{
    public SkittlesRepository(SkittlesDbContext context) : base(context) { }
}
