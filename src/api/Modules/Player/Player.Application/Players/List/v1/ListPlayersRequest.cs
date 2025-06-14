using MediatR;

namespace Skittles.WebApi.Player.Application.Players.List.v1;

public record ListPlayersRequest : IRequest<List<PlayerResponse>>;