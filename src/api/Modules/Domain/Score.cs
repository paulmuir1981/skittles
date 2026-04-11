using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Score : AuditableEntity<(Guid PlayerId, Guid LegId)>, IAggregateRoot
{
    public Guid PlayerId { get; private set; }
    public Guid LegId { get; private set; }
    public byte Value { get; private set; }

    public Player Player { get; private set; } = null!;
    public Leg Leg { get; private set; } = null!;

    public static Score Create(Guid playerId, Guid legId, byte value)
    {
        var score = new Score
        {
            Id = (playerId, legId),
            PlayerId = playerId,
            LegId = legId,
            Value = value
        };

        //score.QueueDomainEvent(new ScoreCreated() { Score = score });

        return score;
    }
}