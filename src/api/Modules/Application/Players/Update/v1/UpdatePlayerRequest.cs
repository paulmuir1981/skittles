using MediatR;
using System.Text.Json.Serialization;

namespace Skittles.WebApi.Application.Players.Update.v1;

public record UpdatePlayerRequest(string? Name, string? Nickname, bool CanDrive) : IRequest<UpdatePlayerResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}