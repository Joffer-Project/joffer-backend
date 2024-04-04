using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos
{
    public class CompanyDto
    {
        public CompanyDto() { }

        public CompanyDto(Company company)
        {
            Id = company.Id;
            AccountId = company.AccountId;
            Description = company.Description;
            LogoUrl = company.LogoUrl;
            RecruiterToken = company.RecruiterToken;
            TokenActiveSince = company.TokenActiveSince;
            IsActive = company.IsActive;
        }

        public int Id { get; set; }

        public int AccountId { get; set; }

        public string Description { get; set; }

        public string? LogoUrl { get; set; }

        public string RecruiterToken { get; set; }

        public DateTime TokenActiveSince { get; set; } 

        public bool IsActive { get; set; }
    }
}
