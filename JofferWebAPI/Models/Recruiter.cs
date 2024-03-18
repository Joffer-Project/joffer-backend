using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class Recruiter
{
    public Recruiter() { }

    public Recruiter(RecruiterDto recruiterDto)
    {
        CompanyId = recruiterDto.CompanyId;
        AccountId = recruiterDto.AccountId;
        IsActive = recruiterDto.IsActive;
    }

    [Key]
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public int AccountId { get; set; }

    public bool IsActive { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<RecruiterToJobOffer> RecruiterToJobOffers { get; set; } = new List<RecruiterToJobOffer>();
}
