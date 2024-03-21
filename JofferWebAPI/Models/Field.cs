using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class Field
{
    public Field() { }  

    public Field(FieldDto fieldDto)
    {
        Name = fieldDto.Name;
        IsActive = fieldDto.IsActive;
    }

    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<AccountDicipline> AccountDiciplines { get; set; } = new List<AccountDicipline>();

    public virtual ICollection<Dicipline> Diciplines { get; set; } = new List<Dicipline>();

    public virtual ICollection<JobOffer> JobOffers { get; set; } = new List<JobOffer>();
}
