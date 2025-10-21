using MediatR;

namespace Skittles.WebApi.Application.Players.List.v1;

public record ListPlayersRequest : IRequest<List<PlayerResponse>>;
