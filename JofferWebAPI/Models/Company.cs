using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class Company
{
    public Company() { }

    public Company (CompanyDto companyDto)
    {
        AccountId = companyDto.AccountId;
        LogoUrl = companyDto.LogoUrl;
        Description = companyDto.Description;
        Image2Url = companyDto.Image2Url;
        Image3Url = companyDto.Image3Url;
        Image4Url = companyDto.Image4Url;
        Image5Url = companyDto.Image5Url;
        ComapnyUrl = companyDto.ComapnyUrl;
        LinkedInUrl = companyDto.LinkedInUrl;
        YoutubeUrl = companyDto.YoutubeUrl;
        InstaGramUrl = companyDto.InstaGramUrl;
        TwitterUrl = companyDto.TwitterUrl;
        IsActive = companyDto.IsActive;
        RecruiterToken = "000000";
        TokenActiveSince = (DateTime.Now).ToUniversalTime();
    }

    [Key]
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


    public string RecruiterToken { get; set; } = null!;

    public DateTime TokenActiveSince { get; set; }

    public bool IsActive { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<JobOffer> JobOffers { get; set; } = new List<JobOffer>();
}
