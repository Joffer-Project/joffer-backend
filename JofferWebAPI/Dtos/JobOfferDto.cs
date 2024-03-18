using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos
{
    public class JobOfferDto
    {
        public JobOfferDto() { }

        public JobOfferDto(JobOffer jobOffer)
        {
            Id = jobOffer.Id;
            CompanyId = jobOffer.CompanyId;
            Title = jobOffer.Title;
            FieldId = jobOffer.FieldId;
            Description = jobOffer.Description;
            Salary = jobOffer.Salary;
            EmploymentStatus = jobOffer.EmploymentStatus;
            IsActive = jobOffer.IsActive;
        }

        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int FieldId { get; set; }

        public string Description { get; set; } = null!;

        public int Salary { get; set; }

        public string EmploymentStatus { get; set; } = null!;

        public int CompanyId { get; set; }

        public bool IsActive { get; set; }
    }
}
