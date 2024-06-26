﻿using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class JobOffer
{
    public JobOffer() { }

    public JobOffer(JobOfferDto jobOfferDto)
    {
        Title = jobOfferDto.Title;
        Description = jobOfferDto.Description;
        MinSalary = jobOfferDto.MinSalary;
        MaxSalary = jobOfferDto.MaxSalary;
        EmploymentStatus = jobOfferDto.EmploymentStatus;
        CompanyId = jobOfferDto.CompanyId;
        IsActive = jobOfferDto.IsActive;
    }

    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int MinSalary { get; set; }

    public int MaxSalary { get; set; }

    public string EmploymentStatus { get; set; } = null!;

    public int CompanyId { get; set; }

    public bool IsActive { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<JobOfferSwipe> JobOfferSwipes { get; set; } = new List<JobOfferSwipe>();

    public List<JobOfferIndustries> JobOfferIndustries { get; set; }

    public List<JobOfferRoles> JobOfferRoles { get; set; }
}
