using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class JobOfferSwipe
{
    public JobOfferSwipe() { }

    public JobOfferSwipe(JobOfferSwipeDto jobOfferSwipeDto) { 
        ApplicantId = jobOfferSwipeDto.ApplicantId;
        JobOfferId = jobOfferSwipeDto.JobOfferId;
        ApplicantInterested = jobOfferSwipeDto.ApplicantInterested;
        FinalMatch = jobOfferSwipeDto.FinalMatch;
        IsActive = jobOfferSwipeDto.IsActive;
    }

    [Key]
    public int Id { get; set; }

    public int ApplicantId { get; set; }

    public int JobOfferId { get; set; }

    public bool ApplicantInterested { get; set; }

    public bool FinalMatch { get; set; }

    public bool IsActive { get; set; }

    public virtual JobOffer JobOffer { get; set; } = null!;
}
