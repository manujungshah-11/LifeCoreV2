using LifeV2.Application.DTOs;

namespace LifeV2.Application.Interfaces;

public interface IPolicyService
{
    Task<IReadOnlyList<PolicyDto>> GetAllAsync(CancellationToken ct = default);
    Task<PolicyDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PolicyDto> CreateAsync(CreatePolicyRequest request, CancellationToken ct = default);
    Task<PolicyDto> UpdateAsync(Guid id, UpdatePolicyRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
