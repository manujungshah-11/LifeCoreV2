namespace LifeV2.Domain.Common;

/// <summary>
/// Base class for all persistable entities. Provides an identity and audit fields.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
}
