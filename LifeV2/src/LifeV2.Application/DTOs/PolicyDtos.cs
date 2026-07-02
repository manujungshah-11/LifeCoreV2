using LifeV2.Domain.Enums;

namespace LifeV2.Application.DTOs;

public record PolicyDto(
    Guid Id,
    string PolicyNumber,
    string ProductName,
    decimal CoverageAmount,
    decimal MonthlyPremium,
    PolicyStatus Status,
    DateTime StartDate,
    DateTime? EndDate,
    Guid PolicyholderId);

public record CreatePolicyRequest(
    string PolicyNumber,
    string ProductName,
    decimal CoverageAmount,
    decimal MonthlyPremium,
    DateTime StartDate,
    Guid PolicyholderId);

public record UpdatePolicyRequest(
    string ProductName,
    decimal CoverageAmount,
    decimal MonthlyPremium,
    PolicyStatus Status,
    DateTime? EndDate);
