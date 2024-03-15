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
    public DbSet<AccountDicipline> AccountDiciplines { get; set; }
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Dicipline> Diciplines { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<JobOffer> JobOffers { get; set; }
    public DbSet<JobOfferSwipe> JobOfferSwipes { get; set; }
    public DbSet<Recruiter> Recruiters { get; set; }
    public DbSet<RecruiterToJobOffer> RecruiterToJobOffers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
