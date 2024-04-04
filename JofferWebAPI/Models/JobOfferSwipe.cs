using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class JobOfferSwipe
{
    public JobOfferSwipe() { }

    public JobOfferSwipe(JobOfferSwipeDto jobOfferSwipeDto) { 
        
        Id = jobOfferSwipeDto.TalentId;
        JobOfferId = jobOfferSwipeDto.JobOfferId;
        TalentInterested = jobOfferSwipeDto.TalentInterested;
        FinalMatch = jobOfferSwipeDto.FinalMatch;
        IsActive = jobOfferSwipeDto.IsActive;
    }

    [Key]
    public int Id { get; set; }

    public int TalentId { get; set; }

    public int JobOfferId { get; set; }

    public bool TalentInterested { get; set; }

    public bool FinalMatch { get; set; }

    public bool IsActive { get; set; }

    public virtual JobOffer JobOffer { get; set; } = null!;
}
