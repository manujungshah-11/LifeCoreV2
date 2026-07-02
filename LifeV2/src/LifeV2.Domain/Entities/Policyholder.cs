using LifeV2.Domain.Common;

namespace LifeV2.Domain.Entities;

public class Policyholder : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    public ICollection<Policy> Policies { get; set; } = new List<Policy>();

    public string FullName => $"{FirstName} {LastName}".Trim();
}
