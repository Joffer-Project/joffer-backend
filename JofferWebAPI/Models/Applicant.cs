using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class Applicant
{
    public Applicant(ApplicantDto applicantDto) { 
        AccountId = applicantDto.AccountId;
        AboutMe = applicantDto.AboutMe;
        SalaryMinimum = applicantDto.SalaryMinimum;
        Avatar = applicantDto.Avatar;
        IsActive = applicantDto.IsActive;
    }

    [Key]
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string AboutMe { get; set; } = null!;

    public int SalaryMinimum { get; set; }

    public string EmploymentStatus { get; set; } = null!;

    public byte[]? Avatar { get; set; }

    public bool IsActive { get; set; }

    public virtual Account Account { get; set; } = null!;
}
