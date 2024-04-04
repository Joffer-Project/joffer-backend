using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos
{
    public class JobOfferSwipeDto
    {
        public JobOfferSwipeDto() { }

        public JobOfferSwipeDto(JobOfferSwipe jobOfferSwipe)
        {
            Id = jobOfferSwipe.Id;
            TalentId = jobOfferSwipe.TalentId;
            JobOfferId = jobOfferSwipe.JobOfferId;
            TalentInterested = jobOfferSwipe.TalentInterested;
            FinalMatch = jobOfferSwipe.FinalMatch;
            IsActive = jobOfferSwipe.IsActive;
        }

        public int Id { get; set; }

        public int TalentId { get; set; }

        public int JobOfferId { get; set; }

        public bool TalentInterested { get; set; }

        public bool FinalMatch { get; set; }

        public bool IsActive { get; set; }
    }
}
