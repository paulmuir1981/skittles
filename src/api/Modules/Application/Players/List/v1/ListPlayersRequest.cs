using MediatR;

namespace Skittles.WebApi.Application.Players.List.v1;

public record ListPlayersRequest(bool IncludeDeleted) : IRequest<List<PlayerResponse>>;
