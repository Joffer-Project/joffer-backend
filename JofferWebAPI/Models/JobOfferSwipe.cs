using System.ComponentModel.DataAnnotations;

namespace JofferWebAPI.Models;

public partial class JobOfferSwipe
{
    [Key]
    public int Id { get; set; }

    public int ApplicantId { get; set; }

    public int JobOfferId { get; set; }

    public bool ApplicantInterested { get; set; }

    public bool FinalMatch { get; set; }

    public bool IsActive { get; set; }

    public virtual JobOffer JobOffer { get; set; } = null!;
}
