using Ardalis.Specification;

namespace Skittles.WebApi.Domain.Specifications;

public sealed class IdSpec : Specification<Player>
{
    public IdSpec(Guid id)
    {
        Query
            .Where(p => p.PlayerId == id);
    }
}