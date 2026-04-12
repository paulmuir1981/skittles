using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Player : AuditableEntity<long>, IAggregateRoot
{
    public Guid PlayerId { get; private set; }
    public string Name { get; private set; } = default!;
    public string Nickname { get; private set; } = default!;
    public bool CanDrive { get; private set; }
    public bool IsDeleted { get; private set; }
    public IReadOnlyList<Score> Scores { get; private set; } = [];
    public IReadOnlyList<Driver> Drivers { get; private set; } = [];
    public IReadOnlyList<Holiday> Holidays { get; private set; } = [];

    public static Player Create(string name, string? nickname = null, bool canDrive = false, bool isDeleted = false)
    {
        var player = new Player
        {
            PlayerId = Guid.NewGuid(),
            Name = name, 
            Nickname = nickname ?? name, 
            CanDrive = canDrive, 
            IsDeleted = isDeleted
        };

        //player.QueueDomainEvent(new PlayerCreated() { Player = player });

        return player;
    }

    public Player Update(string? name, string? nickname, bool canDrive, bool isDeleted)
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
        IsDeleted = isDeleted;

        //this.QueueDomainEvent(new PlayerUpdated() { Player = this });
        return this;
    }
}
