﻿// <auto-generated />
using System;
using JofferWebAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JofferWebAPI.Migrations
{
    [DbContext(typeof(DbContextRender))]
    [Migration("20240404132920_removedRecruiterReferences")]
    partial class removedRecruiterReferences
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JofferWebAPI.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Auth0Id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPremium")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("JofferWebAPI.Models.AccountDicipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<int>("DiciplineId")
                        .HasColumnType("integer");

                    b.Property<int>("FieldId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("DiciplineId");

                    b.HasIndex("FieldId");

                    b.ToTable("AccountDiciplines");
                });

            modelBuilder.Entity("JofferWebAPI.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<string>("ComapnyUrl")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Image2Url")
                        .HasColumnType("text");

                    b.Property<string>("Image3Url")
                        .HasColumnType("text");

                    b.Property<string>("Image4Url")
                        .HasColumnType("text");

                    b.Property<string>("Image5Url")
                        .HasColumnType("text");

                    b.Property<string>("InstaGramUrl")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LinkedInUrl")
                        .HasColumnType("text");

                    b.Property<string>("LogoUrl")
                        .HasColumnType("text");

                    b.Property<string>("RecruiterToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TokenActiveSince")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TwitterUrl")
                        .HasColumnType("text");

                    b.Property<string>("YoutubeUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("JofferWebAPI.Models.Dicipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FieldId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.ToTable("Diciplines");
                });

            modelBuilder.Entity("JofferWebAPI.Models.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("JofferWebAPI.Models.JobOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EmploymentStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FieldId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxSalary")
                        .HasColumnType("integer");

                    b.Property<int>("MinSalary")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("FieldId");

                    b.ToTable("JobOffers");
                });

            modelBuilder.Entity("JofferWebAPI.Models.JobOfferSwipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("FinalMatch")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("JobOfferId")
                        .HasColumnType("integer");

                    b.Property<int>("TalentId")
                        .HasColumnType("integer");

                    b.Property<bool>("TalentInterested")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("JobOfferId");

                    b.ToTable("JobOfferSwipes");
                });

            modelBuilder.Entity("JofferWebAPI.Models.Talent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AboutMe")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text");

                    b.Property<string>("DribbleUrl")
                        .HasColumnType("text");

                    b.Property<string>("EmploymentStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GitHubUrl")
                        .HasColumnType("text");

                    b.Property<string>("Image2Url")
                        .HasColumnType("text");

                    b.Property<string>("Image3Url")
                        .HasColumnType("text");

                    b.Property<string>("Image4Url")
                        .HasColumnType("text");

                    b.Property<string>("Image5Url")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LinkedInUrl")
                        .HasColumnType("text");

                    b.Property<string>("MediumUrl")
                        .HasColumnType("text");

                    b.Property<string>("PersonalUrl")
                        .HasColumnType("text");

                    b.Property<int>("SalaryMinimum")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Talents");
                });

            modelBuilder.Entity("JofferWebAPI.Models.AccountDicipline", b =>
                {
                    b.HasOne("JofferWebAPI.Models.Account", "Account")
                        .WithMany("AccountDiciplines")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JofferWebAPI.Models.Dicipline", "Dicipline")
                        .WithMany("AccountDiciplines")
                        .HasForeignKey("DiciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JofferWebAPI.Models.Field", "Field")
                        .WithMany("AccountDiciplines")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Dicipline");

                    b.Navigation("Field");
                });

            modelBuilder.Entity("JofferWebAPI.Models.Company", b =>
                {
                    b.HasOne("JofferWebAPI.Models.Account", "Account")
                        .WithMany("Companies")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("JofferWebAPI.Models.Dicipline", b =>
                {
                    b.HasOne("JofferWebAPI.Models.Field", "Field")
                        .WithMany("Diciplines")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Field");
                });

            modelBuilder.Entity("JofferWebAPI.Models.JobOffer", b =>
                {
                    b.HasOne("JofferWebAPI.Models.Company", "Company")
                        .WithMany("JobOffers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JofferWebAPI.Models.Field", "Field")
                        .WithMany("JobOffers")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Field");
                });

            modelBuilder.Entity("JofferWebAPI.Models.JobOfferSwipe", b =>
                {
                    b.HasOne("JofferWebAPI.Models.JobOffer", "JobOffer")
                        .WithMany("JobOfferSwipes")
                        .HasForeignKey("JobOfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobOffer");
                });

            modelBuilder.Entity("JofferWebAPI.Models.Talent", b =>
                {
                    b.HasOne("JofferWebAPI.Models.Account", "Account")
                        .WithMany("Talents")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("JofferWebAPI.Models.Account", b =>
                {
                    b.Navigation("AccountDiciplines");

                    b.Navigation("Companies");

                    b.Navigation("Talents");
                });

            modelBuilder.Entity("JofferWebAPI.Models.Company", b =>
                {
                    b.Navigation("JobOffers");
                });

            modelBuilder.Entity("JofferWebAPI.Models.Dicipline", b =>
                {
                    b.Navigation("AccountDiciplines");
                });

            modelBuilder.Entity("JofferWebAPI.Models.Field", b =>
                {
                    b.Navigation("AccountDiciplines");

                    b.Navigation("Diciplines");

                    b.Navigation("JobOffers");
                });

            modelBuilder.Entity("JofferWebAPI.Models.JobOffer", b =>
                {
                    b.Navigation("JobOfferSwipes");
                });
#pragma warning restore 612, 618
        }
    }
}
