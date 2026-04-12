using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Season : BaseEntity, IAggregateRoot, IKeyedEntity
{
    public Guid Id { get; private set; }
    public int Year { get; private set; }

    public IReadOnlyList<Event> Events { get; private set; } = [];

    public static Season Create(int year)
    {
        var season = new Season
        {
            Year = year
        };

        //season.QueueDomainEvent(new SeasonCreated() { Season = season });

        return season;
    }
}