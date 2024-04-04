using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos
{
    public class TalentDto
    {
        public TalentDto() { }   

        public TalentDto(Talent talent) { 
            Id = talent.Id;
            AccountId = talent.AccountId;
            AboutMe = talent.AboutMe;
            AvatarUrl = talent.AvatarUrl;
            EmploymentStatus = talent.EmploymentStatus;
            IsActive = talent.IsActive;
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AboutMe { get; set; }
        public int SalaryMinimum { get; set; }
        public string EmploymentStatus { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
