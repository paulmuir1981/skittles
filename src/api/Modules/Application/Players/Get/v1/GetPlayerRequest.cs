using MediatR;

namespace Skittles.WebApi.Application.Players.Get.v1;

public sealed record GetPlayerRequest(Guid Id) : IRequest<GetPlayerResponse>;