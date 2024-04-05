using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class Talent
{
    public Talent()
    {

    }

    public Talent(TalentDto applicantDto)
    {
        AccountId = applicantDto.AccountId;
        AboutMe = applicantDto.AboutMe;
        EmploymentStatus = applicantDto.EmploymentStatus;
        SalaryMinimum = applicantDto.SalaryMinimum;
        AvatarUrl = applicantDto.AvatarUrl;
        IsActive = applicantDto.IsActive;
    }

    [Key]
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string AboutMe { get; set; } = null!;

    public int SalaryMinimum { get; set; }

    public string EmploymentStatus { get; set; } = null!;

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

    public virtual Account Account { get; set; } = null!;
}
