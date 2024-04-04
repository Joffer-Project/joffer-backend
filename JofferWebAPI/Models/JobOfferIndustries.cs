using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JofferWebAPI.Models
{
    public class JobOfferIndustries
    {

        [Key]
        public int Id { get; set; }

        public int IndustryId { get; set; }

        public int JobOfferId { get; set; } 

        public bool IsActive { get; set; }

        public JobOffer JobOffer { get; set; } = null!;

        public Industry Industry { get; set; } = null!;

    }
}
