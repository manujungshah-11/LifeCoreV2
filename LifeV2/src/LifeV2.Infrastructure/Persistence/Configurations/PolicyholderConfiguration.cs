using LifeV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeV2.Infrastructure.Persistence.Configurations;

public class PolicyholderConfiguration : IEntityTypeConfiguration<Policyholder>
{
    public void Configure(EntityTypeBuilder<Policyholder> builder)
    {
        builder.ToTable("Policyholders");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Ignore(x => x.FullName);
    }
}
