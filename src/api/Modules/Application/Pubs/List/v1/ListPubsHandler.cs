using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;

namespace Skittles.WebApi.Application.Pubs.List.v1;

public sealed class ListPubsHandler(
    [FromKeyedServices("skittles:pubs")] IReadRepository<Pub> repository)
    : IRequestHandler<ListPubsRequest, List<PubResponse>>
{
    public async Task<List<PubResponse>> Handle(ListPubsRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var spec = new ListPubsSpec(request.IncludeDeleted);
        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        return items;
    }

    public class ListPubsSpec : Specification<Pub, PubResponse>
    {
        public ListPubsSpec(bool includeDeleted)
        {
            if (!includeDeleted)
            {
                Query.Where(x => !x.IsDeleted);
            }
            Query.Select(
                x => new PubResponse(
                    x.Id,
                    x.Name,
                    x.Postcode,
                    x.IsDeleted));
        }
    }
}
