using LifeV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeV2.Infrastructure.Persistence.Configurations;

public class BeneficiaryConfiguration : IEntityTypeConfiguration<Beneficiary>
{
    public void Configure(EntityTypeBuilder<Beneficiary> builder)
    {
        builder.ToTable("Beneficiaries");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Relationship).HasMaxLength(100).IsRequired();
        builder.Property(x => x.SharePercentage).HasColumnType("decimal(5,2)");
    }
}
