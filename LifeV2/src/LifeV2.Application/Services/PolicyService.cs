using LifeV2.Application.Common;
using LifeV2.Application.DTOs;
using LifeV2.Application.Interfaces;
using LifeV2.Domain.Interfaces;

namespace LifeV2.Application.Services;

public class PolicyService : IPolicyService
{
    private readonly IPolicyRepository _repository;

    public PolicyService(IPolicyRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PolicyDto>> GetAllAsync(CancellationToken ct = default)
    {
        var policies = await _repository.GetAllAsync(ct);
        return policies.Select(p => p.ToDto()).ToList();
    }

    public async Task<PolicyDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var policy = await _repository.GetByIdAsync(id, ct)
            ?? throw NotFoundException.For(nameof(Domain.Entities.Policy), id);
        return policy.ToDto();
    }

    public async Task<PolicyDto> CreateAsync(CreatePolicyRequest request, CancellationToken ct = default)
    {
        var policy = request.ToEntity();
        var created = await _repository.AddAsync(policy, ct);
        return created.ToDto();
    }

    public async Task<PolicyDto> UpdateAsync(Guid id, UpdatePolicyRequest request, CancellationToken ct = default)
    {
        var policy = await _repository.GetByIdAsync(id, ct)
            ?? throw NotFoundException.For(nameof(Domain.Entities.Policy), id);

        policy.ProductName = request.ProductName;
        policy.CoverageAmount = request.CoverageAmount;
        policy.MonthlyPremium = request.MonthlyPremium;
        policy.Status = request.Status;
        policy.EndDate = request.EndDate;
        policy.UpdatedAtUtc = DateTime.UtcNow;

        await _repository.UpdateAsync(policy, ct);
        return policy.ToDto();
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var policy = await _repository.GetByIdAsync(id, ct)
            ?? throw NotFoundException.For(nameof(Domain.Entities.Policy), id);
        await _repository.DeleteAsync(policy, ct);
    }
}
