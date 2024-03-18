using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class RecruiterToJobOffer
{
    public RecruiterToJobOffer() { }

    public RecruiterToJobOffer(RecruiterToJobOfferDto recruiterToJobOfferDto)
    {
        RecruiterId = recruiterToJobOfferDto.Id;
        JobOfferId = recruiterToJobOfferDto.JobOfferId;
        IsActive = recruiterToJobOfferDto.IsActive;
    }

    [Key]
    public int Id { get; set; }

    public int RecruiterId { get; set; }

    public int JobOfferId { get; set; }

    public bool IsActive { get; set; }

    public virtual JobOffer JobOffer { get; set; } = null!;

    public virtual Recruiter Recruiter { get; set; } = null!;
}
