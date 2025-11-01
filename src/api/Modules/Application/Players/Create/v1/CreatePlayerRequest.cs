using MediatR;

namespace Skittles.WebApi.Application.Players.Create.v1;

public sealed record CreatePlayerRequest(string? Name, string? Nickname, bool CanDrive) : IRequest<CreatePlayerResponse>
{ }
