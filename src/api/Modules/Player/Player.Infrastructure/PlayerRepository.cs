using Microsoft.EntityFrameworkCore;
using Skittles.WebApi.Player.Application;
using Skittles.WebApi.Player.Domain;

namespace Skittles.WebApi.Player.Infrastructure;

public class PlayerRepository : IPlayerRepository
{
    private readonly PlayerDbContext _context;

    public PlayerRepository(PlayerDbContext context)
    {
        _context = context;
    }

    public async Task<Skittles.WebApi.Player.Domain.Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Players
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Skittles.WebApi.Player.Domain.Player>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Players
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Skittles.WebApi.Player.Domain.Player> AddAsync(Skittles.WebApi.Player.Domain.Player entity, CancellationToken cancellationToken = default)
    {
        _context.Players.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Skittles.WebApi.Player.Domain.Player entity, CancellationToken cancellationToken = default)
    {
        _context.Players.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Skittles.WebApi.Player.Domain.Player entity, CancellationToken cancellationToken = default)
    {
        _context.Players.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
