using LifeV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeV2.Infrastructure.Persistence.Configurations;

public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
{
    public void Configure(EntityTypeBuilder<Policy> builder)
    {
        builder.ToTable("Policies");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PolicyNumber).HasMaxLength(50).IsRequired();
        builder.HasIndex(x => x.PolicyNumber).IsUnique();

        builder.Property(x => x.ProductName).HasMaxLength(150).IsRequired();
        builder.Property(x => x.CoverageAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.MonthlyPremium).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Status).HasConversion<int>();

        builder.HasOne(x => x.Policyholder)
            .WithMany(p => p.Policies)
            .HasForeignKey(x => x.PolicyholderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Beneficiaries)
            .WithOne(b => b.Policy!)
            .HasForeignKey(b => b.PolicyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
