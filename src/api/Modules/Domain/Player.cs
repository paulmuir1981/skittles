using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Player : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;

    public static Player Create(string name)
    {
        var player = new Player
        {
            Name = name
        };

        //player.QueueDomainEvent(new PlayerCreated() { Player = player });

        return player;
    }
}
