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
            IsActive = company.IsActive;
        }

        public int Id { get; set; }

        public int AccountId { get; set; }

        public string Description { get; set; } = null!;

        public string? LogoUrl { get; set; }
        public string? Image2Url { get; set; }
        public string? Image3Url { get; set; }
        public string? Image4Url { get; set; }
        public string? Image5Url { get; set; }

        public string? ComapnyUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? YoutubeUrl { get; set; }
        public string? InstaGramUrl { get; set; }
        public string? TwitterUrl { get; set; }

        public bool IsActive { get; set; }
        
        //Account info
        public string Name { get; set; }
        public string Auth0Id { get; set; }
        public string Email { get; set; }
        public bool IsPremium { get; set; }
    }
}
