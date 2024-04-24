using JofferWebAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace JofferWebAPI.Dtos
{
    public class TalentDto
    {
        public TalentDto()
        {
        }

        public TalentDto(Talent talent)
        {
            Id = talent.Id;
            AccountId = talent.AccountId;
            AboutMe = talent.AboutMe;
            SalaryMinimum = talent.SalaryMinimum;
            EmploymentStatus = talent.EmploymentStatus;
            AvatarUrl = talent.AvatarUrl;
            IsActive = talent.IsActive;
            Image2Url = talent.Image2Url;
            Image3Url = talent.Image3Url;
            Image4Url = talent.Image4Url;
            Image5Url = talent.Image5Url;
            GitHubUrl = talent.GitHubUrl;
            LinkedInUrl = talent.LinkedInUrl;
            MediumUrl = talent.MediumUrl;
            DribbleUrl = talent.DribbleUrl;
            PersonalUrl = talent.PersonalUrl;

            Name = talent.Account.Name;
            Auth0Id = talent.Account.Auth0Id;
            Email = talent.Account.Email;
            IsPremium = talent.Account.IsPremium;
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AboutMe { get; set; }
        public int SalaryMinimum { get; set; }
        public string EmploymentStatus { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Image2Url { get; set; }
        public string? Image3Url { get; set; }
        public string? Image4Url { get; set; }
        public string? Image5Url { get; set; }
        public string? GitHubUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? MediumUrl { get; set; }
        public string? DribbleUrl { get; set; }
        public string? PersonalUrl { get; set; }
        public bool IsActive { get; set; }

        //Account info
        public string Name { get; set; }
        public string Auth0Id { get; set; }
        public string Email { get; set; }
        public bool IsPremium { get; set; }
}
}