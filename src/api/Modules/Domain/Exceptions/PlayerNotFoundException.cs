using Skittles.Framework.Core.Exceptions;

namespace Skittles.WebApi.Domain.Exceptions;

public sealed class PlayerNotFoundException : NotFoundException
{
    public PlayerNotFoundException(Guid id) : base($"player with id {id} not found") { }
}
