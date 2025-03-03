using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Catalog.Domain
{
    public class PlayerItem : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; } = default!;
    }
}
