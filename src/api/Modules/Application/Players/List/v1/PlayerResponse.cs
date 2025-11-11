namespace Skittles.WebApi.Application.Players.List.v1;

public sealed record PlayerResponse(Guid Id, string Name, string Nickname, bool CanDrive, bool IsDeleted);
