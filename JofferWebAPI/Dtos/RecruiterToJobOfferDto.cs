namespace JofferWebAPI.Dtos
{
    public class RecruiterToJobOfferDto
    {
        public RecruiterToJobOfferDto() { }

        public RecruiterToJobOfferDto(RecruiterToJobOfferDto recruiterToJobOffer)
        {
            Id = recruiterToJobOffer.Id;
            RecruiterId = recruiterToJobOffer.RecruiterId;
            JobOfferId = recruiterToJobOffer.JobOfferId;
            IsActive = recruiterToJobOffer.IsActive;
        }

        public int Id { get; set; }

        public int RecruiterId { get; set; }

        public int JobOfferId { get; set; }

        public bool IsActive { get; set; }
    }
}
