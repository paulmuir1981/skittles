using Ardalis.Specification;

namespace Skittles.WebApi.Domain.Specifications;

public sealed class SeasonDescriptionSpec : Specification<Event>
{
    public SeasonDescriptionSpec(Guid seasonId, string description)
    {
        Query
            .Where(x => x.SeasonId == seasonId && x.Description.ToLower() == description.ToLower());
    }
}