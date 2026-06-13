using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Holiday : BaseEntity, IAggregateRoot
{
    public Guid PlayerId { get; private set; }
    public DateOnly Date { get; private set; }

    public Player Player { get; private set; } = null!;

    public static Holiday Create(Guid playerId, DateOnly date)
    {
        var holiday = new Holiday
        {
            PlayerId = playerId,
            Date = date
        };

        //holiday.QueueDomainEvent(new HolidayCreated() { Holiday = holiday });

        return holiday;
    }
}