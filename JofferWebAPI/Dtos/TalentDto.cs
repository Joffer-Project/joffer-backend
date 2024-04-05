using JofferWebAPI.Models;

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
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AboutMe { get; set; }
        public int SalaryMinimum { get; set; }
        public string EmploymentStatus { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsActive { get; set; }

        //Account info
        public string Name { get; set; }
        public string Auth0Id { get; set; }
        public string Email { get; set; }
        public bool IsPremium { get; set; }
}
}