using LifeV2.Domain.Entities;
using LifeV2.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LifeV2.Infrastructure.Persistence.Repositories;

public class PolicyRepository : IPolicyRepository
{
    private readonly ApplicationDbContext _context;

    public PolicyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Policy>> GetAllAsync(CancellationToken ct = default)
        => await _context.Policies.AsNoTracking().ToListAsync(ct);

    public async Task<Policy?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.Policies
            .Include(p => p.Beneficiaries)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<Policy> AddAsync(Policy policy, CancellationToken ct = default)
    {
        await _context.Policies.AddAsync(policy, ct);
        await _context.SaveChangesAsync(ct);
        return policy;
    }

    public async Task UpdateAsync(Policy policy, CancellationToken ct = default)
    {
        _context.Policies.Update(policy);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Policy policy, CancellationToken ct = default)
    {
        _context.Policies.Remove(policy);
        await _context.SaveChangesAsync(ct);
    }
}
