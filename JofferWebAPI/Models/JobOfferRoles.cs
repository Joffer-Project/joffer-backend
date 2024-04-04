using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JofferWebAPI.Models
{
    public class JobOfferRoles
    {

        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int JobOfferId { get; set; } 

        public bool IsActive { get; set; }

        public JobOffer JobOffer { get; set; } = null!;

        public Role Role { get; set; } = null!;

    }
}
