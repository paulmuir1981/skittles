using MediatR;

namespace Skittles.WebApi.Application.Players.Delete.v1;

public sealed record DeletePlayerRequest(Guid Id) : IRequest;