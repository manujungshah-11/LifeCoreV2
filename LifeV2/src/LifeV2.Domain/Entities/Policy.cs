using LifeV2.Domain.Common;
using LifeV2.Domain.Enums;

namespace LifeV2.Domain.Entities;

public class Policy : BaseEntity
{
    public string PolicyNumber { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal CoverageAmount { get; set; }
    public decimal MonthlyPremium { get; set; }
    public PolicyStatus Status { get; set; } = PolicyStatus.Draft;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Guid PolicyholderId { get; set; }
    public Policyholder? Policyholder { get; set; }

    public ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();
}
