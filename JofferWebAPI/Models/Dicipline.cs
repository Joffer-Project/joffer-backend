using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class Dicipline
{
    public Dicipline() { }

    public Dicipline(DiciplineDto diciplineDto)
    {
        Name = diciplineDto.Name;
        FieldId = diciplineDto.FieldId;
        IsActive = diciplineDto.IsActive;
    }


    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int FieldId { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AccountDicipline> AccountDiciplines { get; set; } = new List<AccountDicipline>();

    public virtual Field Field { get; set; } = null!;
}
