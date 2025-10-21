using Skittles.WebApi.Player.Domain;

namespace Skittles.WebApi.Player.Application;

public interface IPlayerRepository
{
    Task<Skittles.WebApi.Player.Domain.Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Skittles.WebApi.Player.Domain.Player>> ListAsync(CancellationToken cancellationToken = default);
    Task<Skittles.WebApi.Player.Domain.Player> AddAsync(Skittles.WebApi.Player.Domain.Player entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Skittles.WebApi.Player.Domain.Player entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Skittles.WebApi.Player.Domain.Player entity, CancellationToken cancellationToken = default);
}
