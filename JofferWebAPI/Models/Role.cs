using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class Role
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    //public List<AccountRoles> AccountRoles { get; } = new List<AccountRoles>();
    //public List<JobOfferRoles> JobOfferRoles { get; } = new List<JobOfferRoles>();
}
