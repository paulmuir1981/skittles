using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Player.Domain
{
    public class Player : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; } = default!;
    }
}
