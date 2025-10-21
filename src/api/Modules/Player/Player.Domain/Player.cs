using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Player.Domain
{
    public class Player : AuditableEntity, IAggregateRoot
    {
        // Parameterless constructor for Entity Framework
        private Player() { }

        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; private set; } = default!;

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}
