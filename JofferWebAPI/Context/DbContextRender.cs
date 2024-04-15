using System.Configuration;
using JofferWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JofferWebAPI.Context;

public partial class DbContextRender : DbContext
{
    public DbContextRender()
    {
    }

    public DbContextRender(DbContextOptions<DbContextRender> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<JobOfferIndustries> JobOfferIndustries { get; set; }
    public DbSet<JobOfferRoles> JobOfferRoles { get; set; }
    public DbSet<AccountIndustries> AccountIndustries { get; set; }
    public DbSet<AccountRoles> AccountRoles { get; set; }
    public DbSet<Talent> Talents { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Industry> Industry { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<JobOffer> JobOffers { get; set; }
    public DbSet<JobOfferSwipe> JobOfferSwipes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<JofferWebAPI.Models.Role> Role { get; set; } = default!;
}
