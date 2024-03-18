using System.ComponentModel.DataAnnotations;

namespace JofferWebAPI.Models;

public partial class JobOffer
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int FieldId { get; set; }

    public string Description { get; set; } = null!;

    public int Salary { get; set; }

    public string EmploymentStatus { get; set; } = null!;

    public int CompanyId { get; set; }

    public bool IsActive { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Field Field { get; set; } = null!;

    public virtual ICollection<JobOfferSwipe> JobOfferSwipes { get; set; } = new List<JobOfferSwipe>();

    public virtual ICollection<RecruiterToJobOffer> RecruiterToJobOffers { get; set; } = new List<RecruiterToJobOffer>();
}
