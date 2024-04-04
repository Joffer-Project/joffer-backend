using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JofferWebAPI.Models
{
    public class AccountIndustries
    {

        [Key]
        public int Id { get; set; }

        public int IndustryId { get; set; }

        public int AccountId { get; set; } 

        public bool IsActive { get; set; }


        public Account Account { get; set; } = null!;

        public Industry Industry { get; set; } = null!;
    }
}
