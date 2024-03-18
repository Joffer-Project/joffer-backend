using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos
{
    public class ApplicantDto
    {
        public ApplicantDto(Applicant applicant) { 
            Id = applicant.Id;
            AccountId = applicant.AccountId;
            AboutMe = applicant.AboutMe;
            SalaryMinimum = applicant.SalaryMinimum;
            EmploymentStatus = applicant.EmploymentStatus;
            Avatar = applicant.Avatar;
            IsActive = applicant.IsActive;
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AboutMe { get; set; }
        public int SalaryMinimum { get; set; }
        public string EmploymentStatus { get; set; }
        public byte[]? Avatar { get; set; }
        public bool IsActive { get; set; }
    }
}
