namespace Skittles.WebApi.Application.Players.Get.v1;

public sealed record GetPlayerResponse(Guid Id, string Name, string Nickname, bool CanDrive);
