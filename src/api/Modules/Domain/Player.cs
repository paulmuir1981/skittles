using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Player : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string Nickname { get; private set; } = default!;
    public bool CanDrive { get; private set; }

    public static Player Create(string name, string? nickname = null, bool canDrive = false)
    {
        var player = new Player
        {
            Name = name, Nickname = nickname ?? name, CanDrive = canDrive
        };

        //player.QueueDomainEvent(new PlayerCreated() { Player = player });

        return player;
    }

    public Player Update(string? name, string? nickname, bool canDrive)
    {
        if (name is not null && Name?.Equals(name, StringComparison.OrdinalIgnoreCase) is not true) 
        { 
            Name = name; 
        }
        if (nickname is not null && Nickname?.Equals(nickname, StringComparison.OrdinalIgnoreCase) is not true) 
        { 
            Nickname = nickname; 
        }

        CanDrive = canDrive;

        //this.QueueDomainEvent(new PlayerUpdated() { Player = this });
        return this;
    }
}
