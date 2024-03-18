using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class Company
{
    public Company() { }

    public Company (CompanyDto companyDto)
    {
        AccountId = companyDto.AccountId;
        Logo = companyDto.Logo;
        RecruiterToken = companyDto.RecruiterToken;
        TokenActiveSince = companyDto.TokenActiveSince;
        IsActive = companyDto.IsActive;
    }

    [Key]
    public int Id { get; set; }

    public int AccountId { get; set; }

    public byte[]? Logo { get; set; }

    public string RecruiterToken { get; set; } = null!;

    public DateTime TokenActiveSince { get; set; }

    public bool IsActive { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<JobOffer> JobOffers { get; set; } = new List<JobOffer>();

    public virtual ICollection<Recruiter> Recruiters { get; set; } = new List<Recruiter>();
}
