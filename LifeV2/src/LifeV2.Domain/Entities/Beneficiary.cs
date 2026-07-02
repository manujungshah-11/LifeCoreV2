using LifeV2.Domain.Common;

namespace LifeV2.Domain.Entities;

public class Beneficiary : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public decimal SharePercentage { get; set; }

    public Guid PolicyId { get; set; }
    public Policy? Policy { get; set; }
}
