using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos
{
    public class JobOfferSwipeDto
    {
        public JobOfferSwipeDto() { }

        public JobOfferSwipeDto(JobOfferSwipe jobOfferSwipe)
        {
            Id = jobOfferSwipe.Id;
            ApplicantId = jobOfferSwipe.ApplicantId;
            JobOfferId = jobOfferSwipe.JobOfferId;
            ApplicantInterested = jobOfferSwipe.ApplicantInterested;
            FinalMatch = jobOfferSwipe.FinalMatch;
            IsActive = jobOfferSwipe.IsActive;
        }

        public int Id { get; set; }

        public int ApplicantId { get; set; }

        public int JobOfferId { get; set; }

        public bool ApplicantInterested { get; set; }

        public bool FinalMatch { get; set; }

        public bool IsActive { get; set; }
    }
}
