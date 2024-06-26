﻿using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos
{
    public class JobOfferDto
    {
        public JobOfferDto() { }

        public JobOfferDto(JobOffer jobOffer)
        {
            Id = jobOffer.Id;
            Title = jobOffer.Title;
            Description = jobOffer.Description;
            MinSalary = jobOffer.MinSalary;
            MaxSalary = jobOffer.MaxSalary;
            EmploymentStatus = jobOffer.EmploymentStatus;
            CompanyId = jobOffer.CompanyId;
            IsActive = jobOffer.IsActive;
        }

        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int MinSalary { get; set; }

        public int MaxSalary { get; set; }

        public string EmploymentStatus { get; set; } = null!;

        public int CompanyId { get; set; }

        public bool IsActive { get; set; }
    }
}
