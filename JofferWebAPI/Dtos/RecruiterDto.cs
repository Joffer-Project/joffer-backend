using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos
{
    public class RecruiterDto
    {
        public RecruiterDto() { }

        public RecruiterDto(Recruiter recruiter)
        {
            Id = recruiter.Id;
            CompanyId = recruiter.CompanyId;
            AccountId = recruiter.AccountId;
            IsActive = recruiter.IsActive;
        }

        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int AccountId { get; set; }

        public bool IsActive { get; set; }
    }
}
