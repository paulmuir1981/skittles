using MediatR;

namespace Skittles.WebApi.Application.Pubs.List.v1;

public record ListPubsRequest(bool IncludeDeleted) : IRequest<List<PubResponse>>;
