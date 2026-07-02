using LifeV2.Domain.Entities;

namespace LifeV2.Domain.Interfaces;

public interface IPolicyRepository
{
    Task<IReadOnlyList<Policy>> GetAllAsync(CancellationToken ct = default);
    Task<Policy?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Policy> AddAsync(Policy policy, CancellationToken ct = default);
    Task UpdateAsync(Policy policy, CancellationToken ct = default);
    Task DeleteAsync(Policy policy, CancellationToken ct = default);
}
