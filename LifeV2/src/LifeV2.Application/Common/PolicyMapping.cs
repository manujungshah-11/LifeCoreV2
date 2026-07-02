using LifeV2.Application.DTOs;
using LifeV2.Domain.Entities;

namespace LifeV2.Application.Common;

/// <summary>
/// Lightweight manual mapping. Kept dependency-free on purpose; swap for AutoMapper/Mapster later if desired.
/// </summary>
public static class PolicyMapping
{
    public static PolicyDto ToDto(this Policy p) => new(
        p.Id,
        p.PolicyNumber,
        p.ProductName,
        p.CoverageAmount,
        p.MonthlyPremium,
        p.Status,
        p.StartDate,
        p.EndDate,
        p.PolicyholderId);

    public static Policy ToEntity(this CreatePolicyRequest r) => new()
    {
        PolicyNumber = r.PolicyNumber,
        ProductName = r.ProductName,
        CoverageAmount = r.CoverageAmount,
        MonthlyPremium = r.MonthlyPremium,
        StartDate = r.StartDate,
        PolicyholderId = r.PolicyholderId
    };
}
