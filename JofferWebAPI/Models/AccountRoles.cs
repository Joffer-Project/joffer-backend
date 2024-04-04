using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JofferWebAPI.Models
{
    public class AccountRoles
    {

        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int AccountId { get; set; } 

        public bool IsActive { get; set; }

        public Account Account { get; set; } = null!;

        public Role Role { get; set; } = null!;

    }
}
