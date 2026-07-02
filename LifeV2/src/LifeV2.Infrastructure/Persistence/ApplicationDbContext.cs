using LifeV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LifeV2.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Policy> Policies => Set<Policy>();
    public DbSet<Policyholder> Policyholders => Set<Policyholder>();
    public DbSet<Beneficiary> Beneficiaries => Set<Beneficiary>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Picks up every IEntityTypeConfiguration in this assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
