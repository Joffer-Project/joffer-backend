using JofferWebAPI.Models;
using Microsoft.AspNetCore.Identity;

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
            Image2Url = company.Image2Url;
            Image3Url = company.Image3Url;
            Image4Url = company.Image4Url;
            Image5Url = company.Image5Url;
            ComapnyUrl = company.ComapnyUrl;
            LinkedInUrl = company.LinkedInUrl;
            YoutubeUrl = company.YoutubeUrl;
            InstaGramUrl = company.InstaGramUrl;
            TwitterUrl = company.TwitterUrl;
            IsActive = company.IsActive;

            Name = company.Account.Name;
            Auth0Id = company.Account.Auth0Id;
            Email = company.Account.Email;
            PhoneNumber = company.Account.PhoneNumber;
            IsPremium = company.Account.IsPremium;
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
        public string PhoneNumber { get; set; }
        public bool IsPremium { get; set; }
    }
}
