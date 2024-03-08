using System;
using System.Collections.Generic;
using JofferWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JofferWebAPI.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountDicipline> AccountDiciplines { get; set; }

    public virtual DbSet<Applicant> Applicants { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Dicipline> Diciplines { get; set; }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<JobOffer> JobOffers { get; set; }

    public virtual DbSet<JobOfferSwipe> JobOfferSwipes { get; set; }

    public virtual DbSet<Recruiter> Recruiters { get; set; }

    public virtual DbSet<RecruiterToJobOffer> RecruiterToJobOffers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=studmysql01.fhict.local;Uid=dbi458166;Database=dbi458166;Pwd=Z8m4HS7t5UeU;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accounts");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AccountType)
                .HasColumnType("enum('recruiter','company','applicant')")
                .HasColumnName("account_type");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsPremium).HasColumnName("is_premium");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.ReachByEmail).HasColumnName("reach_by_email");
            entity.Property(e => e.ReachByPhone).HasColumnName("reach_by_phone");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<AccountDicipline>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.FieldId, e.DiciplineId }).HasName("PRIMARY");

            entity.ToTable("account_diciplines");

            entity.HasIndex(e => e.DiciplineId, "dicipline_fk0");

            entity.HasIndex(e => e.FieldId, "field_fk0");

            entity.Property(e => e.AccountId)
                .HasColumnType("int(11)")
                .HasColumnName("account_id");
            entity.Property(e => e.FieldId)
                .HasColumnType("int(11)")
                .HasColumnName("field_id");
            entity.Property(e => e.DiciplineId)
                .HasColumnType("int(11)")
                .HasColumnName("dicipline_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountDiciplines)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("account_fk0");

            entity.HasOne(d => d.Dicipline).WithMany(p => p.AccountDiciplines)
                .HasForeignKey(d => d.DiciplineId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("dicipline_fk0");

            entity.HasOne(d => d.Field).WithMany(p => p.AccountDiciplines)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("field_fk0");
        });

        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("applicants");

            entity.HasIndex(e => e.AccountId, "account_fk2");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AboutMe)
                .HasMaxLength(1048)
                .HasColumnName("about_me");
            entity.Property(e => e.AccountId)
                .HasColumnType("int(11)")
                .HasColumnName("account_id");
            entity.Property(e => e.Avatar).HasColumnName("avatar");
            entity.Property(e => e.EmploymentStatus)
                .HasColumnType("enum('parttime','fulltime')")
                .HasColumnName("employment_status");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.SalaryMinimum)
                .HasColumnType("int(11)")
                .HasColumnName("salary_minimum");

            entity.HasOne(d => d.Account).WithMany(p => p.Applicants)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("account_fk2");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("companies");

            entity.HasIndex(e => e.AccountId, "account_fk1");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AccountId)
                .HasColumnType("int(11)")
                .HasColumnName("account_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.RecruiterToken)
                .HasMaxLength(255)
                .HasColumnName("recruiter_token");
            entity.Property(e => e.TokenActiveSince)
                .HasColumnType("datetime")
                .HasColumnName("token_active_since");

            entity.HasOne(d => d.Account).WithMany(p => p.Companies)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("account_fk1");
        });

        modelBuilder.Entity<Dicipline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("diciplines");

            entity.HasIndex(e => e.FieldId, "field_fk1");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FieldId)
                .HasColumnType("int(11)")
                .HasColumnName("field_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.Field).WithMany(p => p.Diciplines)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("field_fk1");
        });

        modelBuilder.Entity<Field>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("fields");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<JobOffer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("job_offer");

            entity.HasIndex(e => e.CompanyId, "company_fk10");

            entity.HasIndex(e => e.FieldId, "field_fk2");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CompanyId)
                .HasColumnType("int(11)")
                .HasColumnName("company_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.EmploymentStatus)
                .HasColumnType("enum('fulltime','parttime','other')")
                .HasColumnName("employment_status");
            entity.Property(e => e.FieldId)
                .HasColumnType("int(11)")
                .HasColumnName("field_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Salary)
                .HasColumnType("int(11)")
                .HasColumnName("salary");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Company).WithMany(p => p.JobOffers)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("company_fk10");

            entity.HasOne(d => d.Field).WithMany(p => p.JobOffers)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("field_fk2");
        });

        modelBuilder.Entity<JobOfferSwipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("job_offer_swipes");

            entity.HasIndex(e => e.ApplicantId, "applicant_fk1");

            entity.HasIndex(e => e.JobOfferId, "job_offer_fk1");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ApplicantId)
                .HasColumnType("int(11)")
                .HasColumnName("applicant_id");
            entity.Property(e => e.ApplicantInterested).HasColumnName("applicant_interested");
            entity.Property(e => e.FinalMatch).HasColumnName("final_match");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.JobOfferId)
                .HasColumnType("int(11)")
                .HasColumnName("job_offer_id");

            entity.HasOne(d => d.JobOffer).WithMany(p => p.JobOfferSwipes)
                .HasForeignKey(d => d.JobOfferId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("job_offer_fk1");
        });

        modelBuilder.Entity<Recruiter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("recruiters");

            entity.HasIndex(e => e.AccountId, "account_fk3");

            entity.HasIndex(e => e.CompanyId, "company_fk0");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AccountId)
                .HasColumnType("int(11)")
                .HasColumnName("account_id");
            entity.Property(e => e.CompanyId)
                .HasColumnType("int(11)")
                .HasColumnName("company_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");

            entity.HasOne(d => d.Account).WithMany(p => p.Recruiters)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("account_fk3");

            entity.HasOne(d => d.Company).WithMany(p => p.Recruiters)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("company_fk0");
        });

        modelBuilder.Entity<RecruiterToJobOffer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("recruiter_to_job_offer");

            entity.HasIndex(e => e.JobOfferId, "job_offer_fk0");

            entity.HasIndex(e => e.RecruiterId, "recruiter_fk0");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.JobOfferId)
                .HasColumnType("int(11)")
                .HasColumnName("job_offer_id");
            entity.Property(e => e.RecruiterId)
                .HasColumnType("int(11)")
                .HasColumnName("recruiter_id");

            entity.HasOne(d => d.JobOffer).WithMany(p => p.RecruiterToJobOffers)
                .HasForeignKey(d => d.JobOfferId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("job_offer_fk0");

            entity.HasOne(d => d.Recruiter).WithMany(p => p.RecruiterToJobOffers)
                .HasForeignKey(d => d.RecruiterId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("recruiter_fk0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
