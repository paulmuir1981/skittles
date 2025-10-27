using MediatR;

namespace Skittles.WebApi.Application.Players.Create.v1;

public sealed record CreatePlayerRequest(string? Name) : IRequest<CreatePlayerResponse>;
