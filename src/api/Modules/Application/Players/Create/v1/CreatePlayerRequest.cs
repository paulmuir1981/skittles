using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Skittles.WebApi.Application.Players.Create.v1;

public sealed record CreatePlayerRequest(
    [Required(ErrorMessage = "Player name is required")]
    string Name,
    string? Nickname, 
    bool CanDrive, 
    bool IsDeleted) : IRequest<CreatePlayerResponse>{ }
